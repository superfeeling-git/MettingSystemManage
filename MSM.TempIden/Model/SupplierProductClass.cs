using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MSM.TempIden.Model
{
    public partial class SupplierProductClass
    {
        public int Id { get; set; }
        public int? SupplierId { get; set; }
        public int? ClassId { get; set; }

        public virtual ProductClass Class { get; set; }
        public virtual Supplier Supplier { get; set; }
    }
}
