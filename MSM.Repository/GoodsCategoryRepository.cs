using MSM.IRepository;
using MSM.Model;
using MSM.Model.Entity;
using MSM.Repository.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSM.Repository
{
    public class GoodsCategoryRepository : BaseRepository<GoodsCategory, int>, IGoodsCategoryRepository
    {
        public GoodsCategoryRepository(MsmDbContext DbContext)
        : base(DbContext)
        {

        }
    }
}
