using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EQTest.Data
{
    public class DbInitializer
    {
        string _dataPath;
        EQTestContext _dbContext;

        public DbInitializer(EQTestContext context)
        {
            _dbContext = context;
        }

        public void CheckDb()
        {
            if (!CheckDataExistance())
            {
                RecreateDbAsync();
            }

        }

        private bool CheckDataExistance()
        {
            try
            {
                var testRecord = _dbContext.Permits.FirstOrDefault();

                return testRecord != null;
            }
            catch
            {
                return false;
            }

        }

        private void RecreateDbAsync()
        {
            try
            {
                _dbContext.Database.EnsureDeleted();
            }
            catch
            {

            }

            var created = _dbContext.Database.EnsureCreated();
            if (created)
            {
                Seed();
            }
        }

        private void Seed()
        {
            _dbContext.Locations.AddRange(new Models.Location
            {
                Name = "Lot1",
                Latitude = "65.262626",
                Longitude = "54.654555",
                LastModifiedBy = 1,
                LastModifiedDate = DateTime.UtcNow
            });

            _dbContext.Lots.AddRange(new Models.Lot
            {
                Name = "Lot1",
                Number = "1",
                City = "Colombo",
                Color = "Color1",
                Zip = "11850",
                StallCount = 25,
                StallSequence = "2",
                TimePeriod = 3,
                IsOverFolowLot = false,
                Latitude = "65.262626",
                Longitude = "54.654555",
                LastModifiedBy = 1,
                LastModifiedDate = DateTime.UtcNow
            });

            _dbContext.SaveChanges();
            
            _dbContext.LotLocations.AddRange(new Models.LotLocation
            {
                LocationId = 1,
                LotId = 1,
                LastModifiedBy = 1,
                LastModifiedDate = DateTime.UtcNow
            });

            _dbContext.Permits.AddRange(new Models.Permit
            {
                Number = "P1555",
                Name = "Permit 1",
                Price = 0,
                GLCode = "GL520",
                Color = "",
                Map = "",
                Description = "",
                VisibleToPublicFrom = DateTime.UtcNow,
                VisibleToPublicTo = DateTime.UtcNow.AddYears(2),
                PermitValidity = Models.PermitValidity.ValidTo,
                ValidDate1 = DateTime.UtcNow,
                ValidDate2 = DateTime.UtcNow.AddYears(2),
                AllocatedStallNos = 20,
                LastModifiedBy = 1,
                LastModifiedDate = DateTime.UtcNow
            });

            _dbContext.SaveChanges();


            _dbContext.PermitLots.AddRange(new Models.PermitLot
            {
                LotId = 1,
                PermitId = 1
            });

            _dbContext.SaveChanges();
        }
    }
}
