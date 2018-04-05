using Korzh.EasyQuery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EQTest.Models
{
    public class LotLocation
    {
        [EqEntityAttr(UseInResult = false, UseInConditions = false)]
        public int Id { get; set; }

        [EqEntityAttr(UseInResult = false, UseInConditions = false)]
        public int LocationId { get; set; }

        [EqEntityAttr(UseInResult = false, UseInConditions = false)]
        public int LotId { get; set; }

        public virtual Location Location { get; set; }

        public virtual Lot Lot { get; set; }

        [EqEntityAttr(UseInResult = false, UseInConditions = false)]
        public DateTime LastModifiedDate { get; set; }

        [EqEntityAttr(UseInResult = false, UseInConditions = false)]
        public int LastModifiedBy { get; set; }
    }
}
