using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MSM.TempIden.Model
{
    public partial class Admin
    {
        public Admin()
        {
            AdminRole = new HashSet<AdminRole>();
            AuditLog = new HashSet<AuditLog>();
        }

        public int AdminId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime? LastLoginTime { get; set; }
        public string LastLoginIp { get; set; }

        public virtual ICollection<AdminRole> AdminRole { get; set; }
        public virtual ICollection<AuditLog> AuditLog { get; set; }
    }
}
