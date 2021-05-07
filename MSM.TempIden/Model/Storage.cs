using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MSM.TempIden.Model
{
    public partial class Storage
    {
        public Storage()
        {
            ProductOrder = new HashSet<ProductOrder>();
            StorageRegion = new HashSet<StorageRegion>();
        }

        public int StorageId { get; set; }
        public int? StorageTypeId { get; set; }
        public string StorageCode { get; set; }
        public string StorageName { get; set; }
        public bool? StorageStatus { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Area { get; set; }
        public string Address { get; set; }
        public string StorageLocation { get; set; }
        public DateTime? CreateTime { get; set; }
        public string UserName { get; set; }

        public virtual StorageType StorageType { get; set; }
        public virtual ICollection<ProductOrder> ProductOrder { get; set; }
        public virtual ICollection<StorageRegion> StorageRegion { get; set; }
    }
}
