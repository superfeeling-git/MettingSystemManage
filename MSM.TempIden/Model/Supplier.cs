using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MSM.TempIden.Model
{
    public partial class Supplier
    {
        public Supplier()
        {
            ProductOrder = new HashSet<ProductOrder>();
            SupplierProductClass = new HashSet<SupplierProductClass>();
        }

        public int SupplierId { get; set; }
        public string SupplierCode { get; set; }
        public string SupplierLevel { get; set; }
        public string SupplierName { get; set; }
        public string Contact { get; set; }
        public string Tel { get; set; }
        public string Phone { get; set; }
        public string Status { get; set; }
        public string PayType { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Area { get; set; }
        public string Address { get; set; }
        public string Photo { get; set; }
        public DateTime? AddTime { get; set; }

        public virtual ICollection<ProductOrder> ProductOrder { get; set; }
        public virtual ICollection<SupplierProductClass> SupplierProductClass { get; set; }
    }
}
