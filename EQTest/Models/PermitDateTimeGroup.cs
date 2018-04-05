using Korzh.EasyQuery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EQTest.Models
{
    public class PermitDateTimeGroup
    {
        [EqEntityAttr(UseInResult = false, UseInConditions = false)]
        public int Id { get; set; }

        [EqEntityAttr(UseInResult = false, UseInConditions = false)]
        public int PermitDateGroupId { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public PermitDateGroup PermitDateGroup { get; set; }
    }
}
