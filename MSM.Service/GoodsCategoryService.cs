using MSM.IService;
using MSM.Model.Entity;
using MSM.Service.Base;
using MSM.IRepository;
using MSM.Model.Model;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MSM.Service
{
    public class GoodsCategoryService : BaseService<GoodsCategory, int>, IGoodsCategoryService
    {
        private IGoodsCategoryRepository GoodsCategoryRepository;
        public GoodsCategoryService(IGoodsCategoryRepository _GoodsCategoryRepository,IUnitOfWork _unitOfWork)
            :base(_GoodsCategoryRepository, _unitOfWork)
        {
            this.GoodsCategoryRepository = _GoodsCategoryRepository;
        }

        /// <summary>
        /// 存储分类树数据
        /// </summary>
        List<CategoryModel> categoryModels = new List<CategoryModel>();
        
        /// <summary>
        /// 根节点数据
        /// </summary>
        /// <returns></returns>
        public async Task<List<CategoryModel>> getAllData()
        {
            var list = await GoodsCategoryRepository.GetAllAsync();

            foreach (var item in list.Where(m => m.ParentID == 0))
            {
                CategoryModel categoryModel = new CategoryModel();
                categoryModel.value = item.CategoryID;
                categoryModel.label = item.CategoryName;

                //递归方法
                await getChildrenData(categoryModel, list);

                categoryModels.Add(categoryModel);
            }

            return categoryModels;
        }

        /// <summary>
        /// 递归方法
        /// </summary>
        /// <param name="categoryModel"></param>
        /// <param name="goodsCategories"></param>
        /// <returns></returns>
        public async Task getChildrenData(CategoryModel categoryModel, List<GoodsCategory> goodsCategories)
        {
            foreach (var item in goodsCategories.Where(m => m.ParentID == categoryModel.value))
            {
                CategoryModel model = new CategoryModel();
                model.value = item.CategoryID;
                model.label = item.CategoryName;
                categoryModel.children.Add(model);

                await getChildrenData(model, goodsCategories);
            }
        }
    }
}
