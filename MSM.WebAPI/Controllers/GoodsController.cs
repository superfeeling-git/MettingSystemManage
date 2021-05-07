using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MSM.Model.Entity;
using MSM.IService;
using MSM.Model.Model;
using Microsoft.AspNetCore.Authorization;

namespace MSM.WebAPI.Controllers
{
    /// <summary>
    /// 商品管理
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(Policy = "Custom")]
    public class GoodsController : ControllerBase
    {
        private IGoodsService goodsService;
        public GoodsController(IGoodsService _goodsService)
        {
            this.goodsService = _goodsService;
        }

        /// <summary>
        /// 添加商品
        /// </summary>
        /// <param name="goods"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateAsync(GoodsModel goods)
        {
            await goodsService.CreateAsync(new Goods { CategoryID = goods.CategoryID, GoodsID = goods.GoodsID, GoodsMoney = goods.GoodsMoney, GoodsName = goods.GoodsName, GoodsPic = goods.GoodsPic });
            return Ok();
        }

        /// <summary>
        /// 获取全部商品
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/api/test")]
        public IActionResult GetAll()
        {
            return new JsonResult(goodsService.GetAll());
        }
    }
}
