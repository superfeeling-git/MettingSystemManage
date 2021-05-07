using System;
using MSM.IRepository;
using MSM.Model.Entity;
using MSM.Service.Base;
using MSM.IService;
using System.Threading.Tasks;
using MSM.IService.Base;

namespace MSM.Service
{
    public class GoodsService : BaseService<Goods, int>, IGoodsService
    {
        //注入仓储
        private readonly IGoodsRepository GoodsRepository;

        public GoodsService(IGoodsRepository _GoodsRepository, IUnitOfWork _UnitOfWork)
            : base(_GoodsRepository, _UnitOfWork)
        {
            this.GoodsRepository = _GoodsRepository;
            this.UnitOfWork = _UnitOfWork;
        }

        public override Task CreateAsync(Goods Entity)
        {
            return base.CreateAsync(Entity);
        }
    }
}
