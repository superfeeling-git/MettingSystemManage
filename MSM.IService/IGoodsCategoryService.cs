using MSM.IService.Base;
using MSM.Model.Entity;
using MSM.Model.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MSM.IService
{
    public interface IGoodsCategoryService : IBaseService<GoodsCategory, int>
    {
        Task<List<CategoryModel>> getAllData();
    }
}
