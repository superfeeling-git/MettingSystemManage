using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MSM.TempIden.Model
{
    public partial class Dict
    {
        public int DictId { get; set; }
        public string DictName { get; set; }
        public int? DictOrder { get; set; }
        public int? DictType { get; set; }
    }
}
