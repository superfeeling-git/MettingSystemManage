using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MSM.TempIden.Model
{
    public partial class Roles
    {
        public Roles()
        {
            AdminRole = new HashSet<AdminRole>();
            RoleMenu = new HashSet<RoleMenu>();
        }

        public int RoleId { get; set; }
        public string RoleName { get; set; }

        public virtual ICollection<AdminRole> AdminRole { get; set; }
        public virtual ICollection<RoleMenu> RoleMenu { get; set; }
    }
}
