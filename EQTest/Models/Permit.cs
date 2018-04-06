using Korzh.EasyQuery;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EQTest.Models
{
    public class Permit
    {
        [EqEntityAttr(UseInResult = false, UseInConditions = false)]
        public int Id { get; set; }

        public string Number { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string GLCode { get; set; }

        public string Color { get; set; }

        [EqEntityAttr(UseInResult = false, UseInConditions = false)]
        public string Map { get; set; }

        public string Description { get; set; }

        [Display(Name = "VIP Exempt")]
        public bool IsVIPExempt { get; set; }

        [Display(Name = "Visible To Public")]
        public bool IsVisibleToPublic { get; set; }

        public DateTime VisibleToPublicFrom { get; set; }

        public DateTime VisibleToPublicTo { get; set; }

        public PermitValidity PermitValidity { get; set; }

        [Display(Name = "Expires / Valid Through Start")]
        // Valid Through, Expires and Periods
        public DateTime ValidDate1 { get; set; }

        [Display(Name = "Valid To / Valid Through End")]
        // End date of Valid Through
        public DateTime ValidDate2 { get; set; }

        public int AllocatedStallNos { get; set; }

        [Display(Name = "Admin Approve")]
        public bool IsAdminApprove { get; set; }

        [Display(Name = "Time Group")]
        public bool IsTimeGroup { get; set; }

        public bool Renewable { get; set; }

        [Display(Name = "Start Date Change")]
        public bool CanStartDateChange { get; set; }

        [EqEntityAttr(UseInResult = false, UseInConditions = false)]
        public DateTime LastModifiedDate { get; set; }

        [EqEntityAttr(UseInResult = false, UseInConditions = false)]
        public int LastModifiedBy { get; set; }

        public virtual IEnumerable<PermitLot> PermitLots { get; set; }

        public virtual IEnumerable<PermitDateGroup> PermitDateGroups { get; set; }

        // Should not show in the Filter Bar
        [EqEntityAttr(UseInResult = false)]
        public virtual IEnumerable<PermitTypeLink> PermitTypeLink { get; set; }
    }

    public class PermitTypeLink
    {
        public int PermitId { get; set; }

        public int TypeId { get; set; }

        public virtual Permit Permit { get; set; }

        public virtual PermitType PermitType { get; set; }
    }


    public class PermitType
    {
        [EqEntityAttr(UseInResult = false, UseInConditions = false)]
        public int Id { get; set; }

        public string Name { get; set; }

        [EqEntityAttr(UseInResult = false, UseInConditions = false)]
        public DateTime LastModifiedDate { get; set; }

        [EqEntityAttr(UseInResult = false, UseInConditions = false)]
        public int LastModifiedBy { get; set; }

        public IEnumerable<PermitTypeLink> PermitTypeLink { get; set; }
    }

    public enum PermitValidity
    {
        PeriodInDays = 1,
        ValidTo = 2,
        ValidThrough = 3,
        Expires = 4,
        WeekEnd = 5,
        WeekDaysEnd = 6,

    }
}
