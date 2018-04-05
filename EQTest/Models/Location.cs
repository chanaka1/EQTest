using Korzh.EasyQuery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EQTest.Models
{
    public class Location
    {
        [EqEntityAttr(UseInResult = false, UseInConditions = false)]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Longitude { get; set; }

        public string Latitude { get; set; }

        [EqEntityAttr(UseInResult = false, UseInConditions = false)]
        public DateTime LastModifiedDate { get; set; }

        [EqEntityAttr(UseInResult = false, UseInConditions = false)]
        public int LastModifiedBy { get; set; }

        //[EqEntityAttr(UseInResult = false, UseInConditions = false)]
        //public virtual IEnumerable<Role> Roles { get; set; }

        public virtual IEnumerable<LotLocation> LotLocations { get; set; }
    }
}
