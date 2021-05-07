using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MSM.TempIden.Model
{
    public partial class ProductProductProperty
    {
        public int ProductId { get; set; }
        public int ProductPropertyId { get; set; }
        public string ProductPropertyVal { get; set; }

        public virtual Product Product { get; set; }
        public virtual ProductProperty ProductProperty { get; set; }
    }
}
