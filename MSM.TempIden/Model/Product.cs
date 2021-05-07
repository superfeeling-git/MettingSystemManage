using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MSM.TempIden.Model
{
    public partial class Product
    {
        public Product()
        {
            OrderProduct = new HashSet<OrderProduct>();
            ProductProductProperty = new HashSet<ProductProductProperty>();
        }

        public int ProductId { get; set; }
        public int? ClassId { get; set; }
        public int? BrandId { get; set; }
        public string ProductCode { get; set; }
        public string ProductBarCode { get; set; }
        public decimal? BuyPrice { get; set; }
        public decimal? SalePrice { get; set; }
        public string Unit { get; set; }
        public string Spec { get; set; }
        public string Details { get; set; }
        public DateTime? AddTime { get; set; }

        public virtual ProductClass Class { get; set; }
        public virtual ICollection<OrderProduct> OrderProduct { get; set; }
        public virtual ICollection<ProductProductProperty> ProductProductProperty { get; set; }
    }
}
