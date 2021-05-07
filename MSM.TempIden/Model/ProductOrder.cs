using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MSM.TempIden.Model
{
    public partial class ProductOrder
    {
        public ProductOrder()
        {
            AuditLog = new HashSet<AuditLog>();
            OrderProduct = new HashSet<OrderProduct>();
        }

        public int OrderId { get; set; }
        public int? SupplierId { get; set; }
        public int? StorageId { get; set; }
        public string OrderNum { get; set; }
        public string BuyType { get; set; }
        public DateTime? ArrivalTime { get; set; }
        public string Remark { get; set; }
        public DateTime? OrderTime { get; set; }
        public int? CreateBy { get; set; }
        public int? AuditBy { get; set; }
        public byte? OrderStatus { get; set; }

        public virtual Storage Storage { get; set; }
        public virtual Supplier Supplier { get; set; }
        public virtual ICollection<AuditLog> AuditLog { get; set; }
        public virtual ICollection<OrderProduct> OrderProduct { get; set; }
    }
}
