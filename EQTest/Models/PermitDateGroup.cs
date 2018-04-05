using Korzh.EasyQuery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EQTest.Models
{
    public class PermitDateGroup
    {
        [EqEntityAttr(UseInResult = false, UseInConditions = false)]
        public int Id { get; set; }

        [EqEntityAttr(UseInResult = false, UseInConditions = false)]
        public int PermitId { get; set; }

        public PermitDateType PermitDateType { get; set; }

        public DateTime PermitDateValue { get; set; }

        public virtual Permit Permit { get; set; }

        public IEnumerable<PermitDateTimeGroup> PermitDateTimeGroups { get; set; }
    }


    public enum PermitDateType
    {
        DayOfWeek = 1,
        Date = 2,
    }
}
