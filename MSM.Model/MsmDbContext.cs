using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MSM.Model.Entity;

namespace MSM.Model
{
    public class MsmDbContext:
        IdentityDbContext<MsmUser, MsmRoles, long, MsmUserClaims, MsmUserRoles, MsmUserLogins, MsmRoleClaims, MsmUserTokens>
    {
        public MsmDbContext()
        {

        }

        public MsmDbContext(DbContextOptions<MsmDbContext> options)
            :base(options)
        {

        }

        public DbSet<Goods> Goods { get; set; }
        public DbSet<GoodsCategory> GoodsCategory { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<MsmRoleClaims>().ToTable("MsmRoleClaims").HasKey(m => m.Id);
            builder.Entity<MsmRoles>().ToTable("MsmRoles").HasKey(m => m.Id);
            //builder.Entity<MsmUser>().ToTable("MsmUser").HasKey(m => m.Id);
            builder.Entity<MsmUserClaims>().ToTable("MsmUserClaims").HasKey(m => m.Id);
            builder.Entity<MsmUserLogins>().ToTable("MsmUserLogins").HasKey(m => m.LoginProvider);
            builder.Entity<MsmUserRoles>().ToTable("MsmUserRoles").HasKey("UserId", "RoleId");
            builder.Entity<MsmUserTokens>().ToTable("MsmUserTokens").HasKey(m => m.UserId);           

            builder.Entity<GoodsCategory>().ToTable("GoodsCategory").HasKey(m => m.CategoryID);
            builder.Entity<Goods>(build => {
                build.HasKey(m => m.GoodsID);
                build.Property(m => m.GoodsMoney).HasColumnType("money");
                //外键
                build.HasOne<GoodsCategory>(m => m.GoodsCategory).
                WithMany(m => m.Goods).
                HasForeignKey(m => m.CategoryID);
            });

            builder.Entity<MsmUser>(build => {
                build.HasKey(m => m.Id);
                build.ToTable("MsmUser");
                build.Property(m => m.Province).HasMaxLength(50);
                build.Property(m => m.City).HasMaxLength(50);
                build.Property(m => m.Area).HasMaxLength(50);
            });

            builder.Entity<SysConfig>(build =>
            {
                build.HasKey(m => m.id);
                build.Property(m => m.name).HasMaxLength(300);
            });
        }
    }
}
