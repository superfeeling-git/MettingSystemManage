using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MSM.TempIden.Model
{
    public partial class StorageType
    {
        public StorageType()
        {
            Storage = new HashSet<Storage>();
        }

        public int StorageTypeId { get; set; }
        public string StorageTypeCode { get; set; }
        public string StorageTypeName { get; set; }
        public string Remark { get; set; }
        public DateTime? LastEditTime { get; set; }
        public string UserName { get; set; }

        public virtual ICollection<Storage> Storage { get; set; }
    }
}
