using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MSM.TempIden.Model
{
    public partial class erp_1807Context : DbContext
    {
        public erp_1807Context()
        {
        }

        public erp_1807Context(DbContextOptions<erp_1807Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Admin> Admin { get; set; }
        public virtual DbSet<AdminRole> AdminRole { get; set; }
        public virtual DbSet<AuditLog> AuditLog { get; set; }
        public virtual DbSet<Dict> Dict { get; set; }
        public virtual DbSet<Goods> Goods { get; set; }
        public virtual DbSet<Manage> Manage { get; set; }
        public virtual DbSet<Menu> Menu { get; set; }
        public virtual DbSet<OrderProduct> OrderProduct { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductClass> ProductClass { get; set; }
        public virtual DbSet<ProductOrder> ProductOrder { get; set; }
        public virtual DbSet<ProductProductProperty> ProductProductProperty { get; set; }
        public virtual DbSet<ProductProperty> ProductProperty { get; set; }
        public virtual DbSet<RoleMenu> RoleMenu { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<Storage> Storage { get; set; }
        public virtual DbSet<StorageLocation> StorageLocation { get; set; }
        public virtual DbSet<StorageRegion> StorageRegion { get; set; }
        public virtual DbSet<StorageType> StorageType { get; set; }
        public virtual DbSet<Supplier> Supplier { get; set; }
        public virtual DbSet<SupplierProductClass> SupplierProductClass { get; set; }
        public virtual DbSet<Test> Test { get; set; }
        public virtual DbSet<ViewStorage> ViewStorage { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=erp_1807;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>(entity =>
            {
                entity.HasComment("管理员表");

                entity.Property(e => e.AdminId)
                    .HasColumnName("AdminID")
                    .HasComment("管理员ID");

                entity.Property(e => e.LastLoginIp)
                    .HasColumnName("LastLoginIP")
                    .HasMaxLength(50)
                    .HasComment("末次登录IP");

                entity.Property(e => e.LastLoginTime)
                    .HasColumnType("datetime")
                    .HasComment("末次登录时间");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .HasComment("密码");

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .HasComment("用户名");
            });

            modelBuilder.Entity<AdminRole>(entity =>
            {
                entity.ToTable("Admin_Role");

                entity.HasComment("管理员_角色");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasComment("ID");

                entity.Property(e => e.AdminId)
                    .HasColumnName("AdminID")
                    .HasComment("管理员ID");

                entity.Property(e => e.RoleId)
                    .HasColumnName("RoleID")
                    .HasComment("角色ID");

                entity.HasOne(d => d.Admin)
                    .WithMany(p => p.AdminRole)
                    .HasForeignKey(d => d.AdminId)
                    .HasConstraintName("FK_Admin_Role_REFERENCE_Admin");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AdminRole)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_Admin_Role_REFERENCE_Roles");
            });

            modelBuilder.Entity<AuditLog>(entity =>
            {
                entity.HasKey(e => e.LogId);

                entity.HasComment("审核日志");

                entity.Property(e => e.LogId)
                    .HasColumnName("LogID")
                    .HasComment("日志ID");

                entity.Property(e => e.AdminId)
                    .HasColumnName("AdminID")
                    .HasComment("管理员ID");

                entity.Property(e => e.AuditStatus).HasComment("审核状态");

                entity.Property(e => e.AuditTime)
                    .HasColumnType("datetime")
                    .HasComment("审核时间");

                entity.Property(e => e.OrderId)
                    .HasColumnName("OrderID")
                    .HasComment("订单ID");

                entity.Property(e => e.Remark)
                    .HasMaxLength(500)
                    .HasComment("备注");

                entity.HasOne(d => d.Admin)
                    .WithMany(p => p.AuditLog)
                    .HasForeignKey(d => d.AdminId)
                    .HasConstraintName("FK_AuditLog_REFERENCE_Admin");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.AuditLog)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_AuditLog_REFERENCE_ProductOrder");
            });

            modelBuilder.Entity<Dict>(entity =>
            {
                entity.HasComment("字典表");

                entity.Property(e => e.DictId)
                    .HasColumnName("DictID")
                    .HasComment("字典ID");

                entity.Property(e => e.DictName)
                    .HasMaxLength(50)
                    .HasComment("字典名称");

                entity.Property(e => e.DictOrder).HasComment("字典排序");

                entity.Property(e => e.DictType).HasComment("字典类型");
            });

            modelBuilder.Entity<Goods>(entity =>
            {
                entity.Property(e => e.GoodsName).HasMaxLength(50);
            });

            modelBuilder.Entity<Manage>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.AdminId)
                    .HasColumnName("AdminID")
                    .HasComment("管理员ID");

                entity.Property(e => e.LastLoginIp)
                    .HasColumnName("LastLoginIP")
                    .HasMaxLength(50)
                    .HasComment("末次登录IP");

                entity.Property(e => e.LastLoginTime)
                    .HasColumnType("datetime")
                    .HasComment("末次登录时间");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .HasComment("密码");

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .HasComment("用户名");
            });

            modelBuilder.Entity<Menu>(entity =>
            {
                entity.HasKey(e => e.ClassId);

                entity.HasComment("菜单表");

                entity.Property(e => e.ClassId)
                    .HasColumnName("ClassID")
                    .HasComment("分类ID");

                entity.Property(e => e.ClassIntro)
                    .HasMaxLength(50)
                    .HasComment("分类描述");

                entity.Property(e => e.ClassName)
                    .HasMaxLength(50)
                    .HasComment("分类名称");

                entity.Property(e => e.Depth).HasComment("分类级别");

                entity.Property(e => e.LinkUrl)
                    .HasMaxLength(100)
                    .HasComment("菜单URL");

                entity.Property(e => e.ParentId)
                    .HasColumnName("ParentID")
                    .HasComment("父ID");

                entity.Property(e => e.ParentPath)
                    .HasMaxLength(50)
                    .HasComment("分类路径");
            });

            modelBuilder.Entity<OrderProduct>(entity =>
            {
                entity.HasComment("采购单商品");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasComment("ID");

                entity.Property(e => e.BuyCount).HasComment("购买数量");

                entity.Property(e => e.BuyPrice)
                    .HasColumnType("money")
                    .HasComment("购买价格");

                entity.Property(e => e.OrderId)
                    .HasColumnName("OrderID")
                    .HasComment("订单ID");

                entity.Property(e => e.ProductId)
                    .HasColumnName("ProductID")
                    .HasComment("商品ID");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderProduct)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_OrderProduct_REFERENCE_ProductOrder");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderProduct)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_OrderProduct_REFERENCE_Product");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasComment("商品表");

                entity.Property(e => e.ProductId)
                    .HasColumnName("ProductID")
                    .HasComment("商品ID");

                entity.Property(e => e.AddTime)
                    .HasColumnType("datetime")
                    .HasComment("添加时间");

                entity.Property(e => e.BrandId)
                    .HasColumnName("BrandID")
                    .HasComment("品牌ID");

                entity.Property(e => e.BuyPrice)
                    .HasColumnType("money")
                    .HasComment("进货价格");

                entity.Property(e => e.ClassId)
                    .HasColumnName("ClassID")
                    .HasComment("分类ID");

                entity.Property(e => e.Details).HasComment("商品介绍");

                entity.Property(e => e.ProductBarCode)
                    .HasMaxLength(50)
                    .HasComment("商品条码");

                entity.Property(e => e.ProductCode)
                    .HasMaxLength(50)
                    .HasComment("商品编码");

                entity.Property(e => e.SalePrice)
                    .HasColumnType("money")
                    .HasComment("销售价格");

                entity.Property(e => e.Spec)
                    .HasColumnName("SPEC")
                    .HasMaxLength(50)
                    .HasComment("规格");

                entity.Property(e => e.Unit)
                    .HasMaxLength(50)
                    .HasComment("计量单位");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.ClassId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_Product_REFERENCE_ProductClass");
            });

            modelBuilder.Entity<ProductClass>(entity =>
            {
                entity.HasKey(e => e.ClassId);

                entity.HasComment("商品分类表");

                entity.Property(e => e.ClassId)
                    .HasColumnName("ClassID")
                    .HasComment("分类ID");

                entity.Property(e => e.ClassIntro)
                    .HasMaxLength(50)
                    .HasComment("分类描述");

                entity.Property(e => e.ClassName)
                    .HasMaxLength(50)
                    .HasComment("分类名称");

                entity.Property(e => e.Depth).HasComment("分类级别");

                entity.Property(e => e.ParentId)
                    .HasColumnName("ParentID")
                    .HasComment("父ID");

                entity.Property(e => e.ParentPath)
                    .HasMaxLength(50)
                    .HasComment("分类路径");
            });

            modelBuilder.Entity<ProductOrder>(entity =>
            {
                entity.HasKey(e => e.OrderId);

                entity.HasComment("采购单");

                entity.Property(e => e.OrderId)
                    .HasColumnName("OrderID")
                    .HasComment("订单ID");

                entity.Property(e => e.ArrivalTime)
                    .HasColumnType("datetime")
                    .HasComment("到货时间");

                entity.Property(e => e.AuditBy).HasComment("审核人");

                entity.Property(e => e.BuyType)
                    .HasMaxLength(50)
                    .HasComment("采购类型");

                entity.Property(e => e.CreateBy).HasComment("创建人");

                entity.Property(e => e.OrderNum)
                    .HasMaxLength(50)
                    .HasComment("订单编号");

                entity.Property(e => e.OrderStatus).HasComment("订单状态");

                entity.Property(e => e.OrderTime)
                    .HasColumnType("datetime")
                    .HasComment("订单创建时间");

                entity.Property(e => e.Remark).HasComment("备注");

                entity.Property(e => e.StorageId)
                    .HasColumnName("StorageID")
                    .HasComment("仓库ID");

                entity.Property(e => e.SupplierId)
                    .HasColumnName("SupplierID")
                    .HasComment("供应商ID");

                entity.HasOne(d => d.Storage)
                    .WithMany(p => p.ProductOrder)
                    .HasForeignKey(d => d.StorageId)
                    .HasConstraintName("FK_ProductOrder_REFERENCE_Storage");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.ProductOrder)
                    .HasForeignKey(d => d.SupplierId)
                    .HasConstraintName("FK_ProductOrder_REFERENCE_Supplier");
            });

            modelBuilder.Entity<ProductProductProperty>(entity =>
            {
                entity.HasKey(e => new { e.ProductId, e.ProductPropertyId });

                entity.ToTable("Product_ProductProperty");

                entity.HasComment("商品_属性表");

                entity.Property(e => e.ProductId)
                    .HasColumnName("ProductID")
                    .HasComment("商品ID");

                entity.Property(e => e.ProductPropertyId)
                    .HasColumnName("ProductPropertyID")
                    .HasComment("属性ID");

                entity.Property(e => e.ProductPropertyVal)
                    .HasMaxLength(50)
                    .HasComment("属性值");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductProductProperty)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Product_ProductProperty_REFERENCE_Product");

                entity.HasOne(d => d.ProductProperty)
                    .WithMany(p => p.ProductProductProperty)
                    .HasForeignKey(d => d.ProductPropertyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Product_ProductProperty_REFERENCE_ProductProperty");
            });

            modelBuilder.Entity<ProductProperty>(entity =>
            {
                entity.HasComment("属性表");

                entity.Property(e => e.ProductPropertyId)
                    .HasColumnName("ProductPropertyID")
                    .HasComment("属性ID");

                entity.Property(e => e.ProductPropertyCode)
                    .HasMaxLength(50)
                    .HasComment("属性编码");

                entity.Property(e => e.ProductPropertyIntro)
                    .HasMaxLength(50)
                    .HasComment("属性描述");

                entity.Property(e => e.ProductPropertyName)
                    .HasMaxLength(50)
                    .HasComment("描述属性");

                entity.Property(e => e.ProductPropertyOrder).HasComment("属性排序");
            });

            modelBuilder.Entity<RoleMenu>(entity =>
            {
                entity.ToTable("Role_Menu");

                entity.HasComment("角色_菜单");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasComment("ID");

                entity.Property(e => e.ClassId)
                    .HasColumnName("ClassID")
                    .HasComment("分类ID");

                entity.Property(e => e.RoleId)
                    .HasColumnName("RoleID")
                    .HasComment("角色ID");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.RoleMenu)
                    .HasForeignKey(d => d.ClassId)
                    .HasConstraintName("FK_Role_Menu_REFERENCE_Menu");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.RoleMenu)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_Role_Menu_REFERENCE_Roles");
            });

            modelBuilder.Entity<Roles>(entity =>
            {
                entity.HasKey(e => e.RoleId);

                entity.HasComment("角色");

                entity.Property(e => e.RoleId)
                    .HasColumnName("RoleID")
                    .HasComment("角色ID");

                entity.Property(e => e.RoleName)
                    .HasMaxLength(50)
                    .HasComment("角色名称");
            });

            modelBuilder.Entity<Storage>(entity =>
            {
                entity.HasComment("仓库");

                entity.Property(e => e.StorageId)
                    .HasColumnName("StorageID")
                    .HasComment("仓库ID");

                entity.Property(e => e.Address)
                    .HasMaxLength(50)
                    .HasComment("地址");

                entity.Property(e => e.Area)
                    .HasMaxLength(50)
                    .HasComment("区");

                entity.Property(e => e.City)
                    .HasMaxLength(50)
                    .HasComment("市");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasComment("创建时间");

                entity.Property(e => e.Province)
                    .HasMaxLength(50)
                    .HasComment("省");

                entity.Property(e => e.StorageCode)
                    .HasMaxLength(50)
                    .HasComment("编号");

                entity.Property(e => e.StorageLocation)
                    .HasMaxLength(50)
                    .HasComment("库位");

                entity.Property(e => e.StorageName)
                    .HasMaxLength(50)
                    .HasComment("名称");

                entity.Property(e => e.StorageStatus).HasComment("状态");

                entity.Property(e => e.StorageTypeId)
                    .HasColumnName("StorageTypeID")
                    .HasComment("类型ID");

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .HasComment("创建人");

                entity.HasOne(d => d.StorageType)
                    .WithMany(p => p.Storage)
                    .HasForeignKey(d => d.StorageTypeId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Storage_REFERENCE_StorageType");
            });

            modelBuilder.Entity<StorageLocation>(entity =>
            {
                entity.HasKey(e => e.LocationId);

                entity.HasComment("库位");

                entity.Property(e => e.LocationId)
                    .HasColumnName("LocationID")
                    .HasComment("库位ID");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasComment("创建时间");

                entity.Property(e => e.LoationName)
                    .HasMaxLength(50)
                    .HasComment("名称");

                entity.Property(e => e.LocationCode)
                    .HasMaxLength(50)
                    .HasComment("编号");

                entity.Property(e => e.LocatonNum)
                    .HasMaxLength(50)
                    .HasComment("编码");

                entity.Property(e => e.MaxVol).HasComment("库容量");

                entity.Property(e => e.RegionId)
                    .HasColumnName("RegionID")
                    .HasComment("库区ID");

                entity.Property(e => e.StorageStatus).HasComment("状态");

                entity.Property(e => e.UserName2)
                    .HasMaxLength(50)
                    .HasComment("创建人");

                entity.HasOne(d => d.Region)
                    .WithMany(p => p.StorageLocation)
                    .HasForeignKey(d => d.RegionId)
                    .HasConstraintName("FK_StorageLocation_REFERENCE_StorageRegion");
            });

            modelBuilder.Entity<StorageRegion>(entity =>
            {
                entity.HasKey(e => e.RegionId);

                entity.HasComment("库区");

                entity.Property(e => e.RegionId)
                    .HasColumnName("RegionID")
                    .HasComment("库区ID");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasComment("创建时间");

                entity.Property(e => e.RegionCode)
                    .HasMaxLength(50)
                    .HasComment("编号");

                entity.Property(e => e.StorageId)
                    .HasColumnName("StorageID")
                    .HasComment("仓库ID");

                entity.Property(e => e.StorageName)
                    .HasMaxLength(50)
                    .HasComment("名称");

                entity.Property(e => e.StorageStatus).HasComment("状态");

                entity.Property(e => e.UserName2)
                    .HasMaxLength(50)
                    .HasComment("创建人");

                entity.HasOne(d => d.Storage)
                    .WithMany(p => p.StorageRegion)
                    .HasForeignKey(d => d.StorageId)
                    .HasConstraintName("FK_StorageRegion_REFERENCE_Storage");
            });

            modelBuilder.Entity<StorageType>(entity =>
            {
                entity.HasComment("仓库类型");

                entity.Property(e => e.StorageTypeId)
                    .HasColumnName("StorageTypeID")
                    .HasComment("类型ID");

                entity.Property(e => e.LastEditTime)
                    .HasColumnType("datetime")
                    .HasComment("最后编辑时间");

                entity.Property(e => e.Remark)
                    .HasMaxLength(500)
                    .HasComment("备注");

                entity.Property(e => e.StorageTypeCode)
                    .HasMaxLength(50)
                    .HasComment("类型编号");

                entity.Property(e => e.StorageTypeName)
                    .HasMaxLength(50)
                    .HasComment("名称");

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .HasComment("最后编辑人");
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.HasComment("供应商");

                entity.Property(e => e.SupplierId)
                    .HasColumnName("SupplierID")
                    .HasComment("供应商ID");

                entity.Property(e => e.AddTime)
                    .HasColumnType("datetime")
                    .HasComment("添加时间");

                entity.Property(e => e.Address)
                    .HasMaxLength(50)
                    .HasComment("地址");

                entity.Property(e => e.Area)
                    .HasMaxLength(50)
                    .HasComment("区");

                entity.Property(e => e.City)
                    .HasMaxLength(50)
                    .HasComment("市");

                entity.Property(e => e.Contact)
                    .HasMaxLength(50)
                    .HasComment("联系人");

                entity.Property(e => e.PayType)
                    .HasMaxLength(50)
                    .HasComment("付款类型");

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .HasComment("手机号");

                entity.Property(e => e.Photo)
                    .HasMaxLength(500)
                    .HasComment("合同");

                entity.Property(e => e.Province)
                    .HasMaxLength(50)
                    .HasComment("省");

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .HasComment("供应商状态");

                entity.Property(e => e.SupplierCode).HasMaxLength(50);

                entity.Property(e => e.SupplierLevel)
                    .HasMaxLength(50)
                    .HasComment("供应商等级");

                entity.Property(e => e.SupplierName)
                    .HasMaxLength(50)
                    .HasComment("名称");

                entity.Property(e => e.Tel)
                    .HasColumnName("TEL")
                    .HasMaxLength(50)
                    .HasComment("电话");
            });

            modelBuilder.Entity<SupplierProductClass>(entity =>
            {
                entity.ToTable("Supplier_ProductClass");

                entity.HasComment("供应商_分类表");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasComment("ID");

                entity.Property(e => e.ClassId)
                    .HasColumnName("ClassID")
                    .HasComment("分类ID");

                entity.Property(e => e.SupplierId)
                    .HasColumnName("SupplierID")
                    .HasComment("供应商ID");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.SupplierProductClass)
                    .HasForeignKey(d => d.ClassId)
                    .HasConstraintName("FK_Supplier_ProductClass_REFERENCE_ProductClass");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.SupplierProductClass)
                    .HasForeignKey(d => d.SupplierId)
                    .HasConstraintName("FK_Supplier_ProductClass_REFERENCE_Supplier");
            });

            modelBuilder.Entity<Test>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<ViewStorage>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("View_Storage");

                entity.Property(e => e.Address).HasMaxLength(50);

                entity.Property(e => e.Area).HasMaxLength(50);

                entity.Property(e => e.City).HasMaxLength(50);

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.Province).HasMaxLength(50);

                entity.Property(e => e.StorageCode).HasMaxLength(50);

                entity.Property(e => e.StorageId).HasColumnName("StorageID");

                entity.Property(e => e.StorageLocation).HasMaxLength(50);

                entity.Property(e => e.StorageName).HasMaxLength(50);

                entity.Property(e => e.StorageTypeId).HasColumnName("StorageTypeID");

                entity.Property(e => e.StorageTypeName).HasMaxLength(50);

                entity.Property(e => e.UserName).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
