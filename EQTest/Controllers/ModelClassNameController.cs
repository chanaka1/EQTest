using System;
using System.Collections.Generic;

using System.Linq;

using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Korzh.EasyQuery;
using Korzh.EasyQuery.Services;
using Korzh.EasyQuery.Linq;
using Korzh.EasyQuery.AspNetCore;
using EQTest.Data;
using EQTest.Models;
using System.IO;
using Korzh.EasyQuery.EntityFrameworkCore;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;

namespace eqtest.Controllers
{
    public class ModelClassNameController : Controller
    {
        EQTestContext dbContext;

        public ModelClassNameController(EQTestContext context) {
            this.dbContext = context;
        }

        // GET: /Order/
        public IActionResult Index() {
            return View("ModelClassNames");
        }

        /// <summary>
        /// Gets the model by its ID
        /// </summary>
        /// <param name="jsonDict">The JsonDict object which contains request parameters</param>
        /// <returns><see cref="IActionResult"/> object with JSON representation of the model</returns>
        [HttpPost]
        public IActionResult GetModel([FromBody] JsonDict jsonDict) {
            string modelId = jsonDict["modelId"].ToString();
            var eqServicez = new EqServiceProviderDb
            {
                ModelLoader = (model, modelName) =>
                {
                    model.LoadFromEntityType(typeof(Permit));
                    model.SortEntities();
                }
            };
            var entityModel = eqServicez.GetModel(modelId);
            return Json(entityModel.SaveToJsonDict());
        }


        /// <summary>
        /// Gets the query by its ID
        /// </summary>
        /// <param name="jsonDict">The JsonDict object which contains request parameters</param>
        /// <returns><see cref="IActionResult"/> object with JSON representation of the query</returns>
        [HttpPost]
        public IActionResult GetQuery([FromBody] JsonDict jsonDict) {
            var eqService = new EqServiceProviderDb
            {
                ModelLoader = (model, modelName) =>
                {
                    model.LoadFromEntityType(typeof(Permit));
                    model.SortEntities();
                }
            };
            var query = eqService.GetQueryByJsonDict(jsonDict);
            return Json(query.SaveToJsonDict());
        }


        /// <summary>
        /// This action returns a custom list by different list request options (list name).
        /// </summary>
        /// <param name="jsonDict">GetList request options.</param>
        /// <returns><see cref="IActionResult"/> object</returns>
        [HttpPost]
        public IActionResult GetList([FromBody] JsonDict jsonDict) {
            var eqService = new EqServiceProviderDb
            {
                ModelLoader = (model, modelName) =>
                {
                    model.LoadFromEntityType(typeof(Permit));
                    model.SortEntities();
                }
            };
            return Json(eqService.GetList(jsonDict, dbContext.Permits));
        }


        [HttpPost]
        public FileStreamResult SaveQueryToFile(string queryJson)
        {
            var eqService = new EqServiceProviderDb
            {
                ModelLoader = (model, modelName) =>
                {
                    model.LoadFromEntityType(typeof(Permit));
                    model.SortEntities();
                }
            };
            var query = eqService.GetQueryByJsonDict(queryJson.ToJsonDict());
            MemoryStream stream = new MemoryStream();
            query.SaveToStream(stream);
            stream.Position = 0;
            return new FileStreamResult(stream, new MediaTypeHeaderValue("text/xml").MediaType)
            {
                FileDownloadName = "CurrentQuery.xml"
            };

        }

        [HttpPost]
        public IActionResult LoadQueryFromFile(string modelId, IFormFile queryFile)
        {
            if (queryFile != null && queryFile.Length > 0)
                try
                {
                    var eqService = new EqServiceProviderDb
                    {
                        ModelLoader = (model, modelName) =>
                        {
                            model.LoadFromEntityType(typeof(Permit));
                            model.SortEntities();
                        }
                    };
                    var query = eqService.GetQuery(modelId, null);
                    using (var fileStream = queryFile.OpenReadStream())
                    {
                        query.LoadFromStream(fileStream);
                    }

                    //saves loaded query into session so it will be loaded automatically after redirect
                    eqService.SyncQuery(query);
                    return RedirectToAction("Index", new { queryId = query.ID });
                }
                catch
                {
                    //just do nothing  
                }
            else
            {

            }

            return RedirectToAction("Index");
        }

        /// <summary>
        /// This action is called when user clicks on "Apply" button in FilterBar or other data-filtering widget
        /// </summary>
        /// <param name="jsonDict"></param>
        /// <returns>IActionResult which contains a partial view with the filtered result set</returns>
        public IActionResult ApplyFilter([FromBody] JsonDict jsonDict) {
            var queryDict = jsonDict["query"] as JsonDict;
            var optionsDict = jsonDict["options"] as JsonDict;
            var eqService = new EqServiceProviderDb
            {
                ModelLoader = (model, modelName) =>
                {
                    model.LoadFromEntityType(typeof(Permit));
                    model.SortEntities();
                }
            };

            var query = eqService.GetQueryByJsonDict(queryDict);
            var lvo = optionsDict.ToListViewOptions();

            // New eq service
            var eqServiceContext = new EqServiceProviderDb
            {
                ModelLoader = (model, modelName) =>
                {
                    (model as Korzh.EasyQuery.Db.DbModel).LoadFromDbContext(this.dbContext);
                    model.SortEntities();
                }
            };

            eqServiceContext.ConnectionResolver = () => {
                return dbContext.Database.GetDbConnection();
            };

            // Save to File
            MemoryStream stream = new MemoryStream();
            query.SaveToStream(stream);
            stream.Position = 0;
            var test = true;
            if (test)
            {
                return new FileStreamResult(stream, new MediaTypeHeaderValue("text/xml").MediaType)
                {
                    FileDownloadName = "CurrentQuery.xml"
                };
            }

            // Read from file
            var xmlQuery = eqServiceContext.GetQuery("", null);
            using (var fileStream = stream)
            {
                xmlQuery.LoadFromStream(fileStream);
            }

            //eqService.SyncQuery(xmlQuery);
            //var xmlJsonDict = xmlQuery.SaveToJsonDict();
            //var xmlqueryDict = xmlJsonDict["query"] as JsonDict;
            //var xmloptionsDict = xmlJsonDict["options"] as JsonDict;

            // Load from file 
            var xmlqueryDict = xmlQuery.SaveToJsonDict();
            eqServiceContext.LoadOptions(xmlqueryDict);
            var xmlquery2 = eqServiceContext.GetQueryByJsonDict(xmlqueryDict);
            var xmlqbr = eqServiceContext.BuildQuery(xmlquery2);
            var resultSet = eqServiceContext.ExecuteQuery(xmlquery2);


            var list = dbContext.Permits
                .Include(x => x.PermitLots).ThenInclude(y => y.Lot).ThenInclude(y => y.LotLocations).ThenInclude(y => y.Location)
                .Include(x => x.PermitDateGroups).ThenInclude(z => z.PermitDateTimeGroups)
                // Add here properties of your model
                //     .Include(c => c.) 
                //     .Include(c => c.)
                .DynamicQuery<Permit>(query, lvo.SortBy).ToPagedList(lvo.PageIndex, 20);

            return View("_ModelClassNameListPartial", list);
        }


    }
}
