using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MSM.TempIden.Model
{
    public partial class ProductClass
    {
        public ProductClass()
        {
            Product = new HashSet<Product>();
            SupplierProductClass = new HashSet<SupplierProductClass>();
        }

        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public string ClassIntro { get; set; }
        public int? Depth { get; set; }
        public int? ParentId { get; set; }
        public string ParentPath { get; set; }

        public virtual ICollection<Product> Product { get; set; }
        public virtual ICollection<SupplierProductClass> SupplierProductClass { get; set; }
    }
}
