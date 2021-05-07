using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MSM.TempIden.Model
{
    public partial class ProductProperty
    {
        public ProductProperty()
        {
            ProductProductProperty = new HashSet<ProductProductProperty>();
        }

        public int ProductPropertyId { get; set; }
        public string ProductPropertyCode { get; set; }
        public string ProductPropertyName { get; set; }
        public string ProductPropertyIntro { get; set; }
        public int? ProductPropertyOrder { get; set; }

        public virtual ICollection<ProductProductProperty> ProductProductProperty { get; set; }
    }
}
