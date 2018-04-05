using Korzh.EasyQuery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EQTest.Models
{
    public class Lot
    {
        [EqEntityAttr(UseInResult = false, UseInConditions = false)]
        public int Id { get; set; }

        public string Number { get; set; }

        public string Name { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Zip { get; set; }

        public int StallCount { get; set; }

        public string StallSequence { get; set; }

        public bool IsTimeLot { get; set; }

        public int TimePeriod { get; set; }

        public string Image { get; set; }

        public string Color { get; set; }

        public string Longitude { get; set; }

        public string Latitude { get; set; }

        public bool IsOverFolowLot { get; set; }

        [EqEntityAttr(UseInResult = false, UseInConditions = false)]
        public DateTime LastModifiedDate { get; set; }

        [EqEntityAttr(UseInResult = false, UseInConditions = false)]
        public int LastModifiedBy { get; set; }

        public virtual IEnumerable<LotLocation> LotLocations { get; set; }

        public virtual IEnumerable<PermitLot> PermitLots { get; set; }
    }
}
