using System;
using System.Collections.Generic;
using System.Text;
using MSM.Model;
using MSM.IRepository;
using MSM.Repository.Base;
using MSM.Model.Entity;
using System.Threading.Tasks;
using System.Linq;

namespace MSM.Repository
{
    public class GoodsRepository : BaseRepository<Goods, int>, IGoodsRepository        
    {
        public GoodsRepository(MsmDbContext DbContext)
        : base(DbContext)
        {

        }

        public override List<Goods> GetAll()
        {
            return base.GetAll();
        }
    }
}
