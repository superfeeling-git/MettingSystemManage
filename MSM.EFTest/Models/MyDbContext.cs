using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MSM.EFTest.Models
{
    public class MyDbContext: DbContext
    {
        public MyDbContext():
            base("Data Source=.;Initial Catalog=db_20210428;Integrated Security=True")
        {

        }

        public DbSet<SysConfig> MyProperty { get; set; }
    }
}