using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MSM.TempIden.Model
{
    public partial class StorageLocation
    {
        public int LocationId { get; set; }
        public int? RegionId { get; set; }
        public string LocationCode { get; set; }
        public string LocatonNum { get; set; }
        public string LoationName { get; set; }
        public int? MaxVol { get; set; }
        public bool? StorageStatus { get; set; }
        public DateTime? CreateTime { get; set; }
        public string UserName2 { get; set; }

        public virtual StorageRegion Region { get; set; }
    }
}
