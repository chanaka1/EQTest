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
               // Seed();
            }
        }
    }
}
