using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MSM.TempIden.Model
{
    public partial class RoleMenu
    {
        public int Id { get; set; }
        public int? RoleId { get; set; }
        public int? ClassId { get; set; }

        public virtual Menu Class { get; set; }
        public virtual Roles Role { get; set; }
    }
}
