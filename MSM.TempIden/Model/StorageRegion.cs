using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MSM.TempIden.Model
{
    public partial class StorageRegion
    {
        public StorageRegion()
        {
            StorageLocation = new HashSet<StorageLocation>();
        }

        public int RegionId { get; set; }
        public int? StorageId { get; set; }
        public string RegionCode { get; set; }
        public string StorageName { get; set; }
        public bool? StorageStatus { get; set; }
        public DateTime? CreateTime { get; set; }
        public string UserName2 { get; set; }

        public virtual Storage Storage { get; set; }
        public virtual ICollection<StorageLocation> StorageLocation { get; set; }
    }
}
