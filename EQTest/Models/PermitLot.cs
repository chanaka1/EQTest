using Korzh.EasyQuery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EQTest.Models
{
    public class PermitLot
    {
        [EqEntityAttr(UseInResult = false, UseInConditions = false)]
        public int PermitId { get; set; }

        [EqEntityAttr(UseInResult = false, UseInConditions = false)]
        public int LotId { get; set; }

        public virtual Permit Permit { get; set; }

        public virtual Lot Lot { get; set; }
    }
}
