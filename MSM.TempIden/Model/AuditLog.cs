using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MSM.TempIden.Model
{
    public partial class AuditLog
    {
        public int LogId { get; set; }
        public int? AdminId { get; set; }
        public int? OrderId { get; set; }
        public DateTime? AuditTime { get; set; }
        public byte? AuditStatus { get; set; }
        public string Remark { get; set; }

        public virtual Admin Admin { get; set; }
        public virtual ProductOrder Order { get; set; }
    }
}
