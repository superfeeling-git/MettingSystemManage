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
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace MSM.WebAPI.Controllers
{
    /// <summary>
    /// 商品管理
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    //[Authorize]
    public class GoodsController : ControllerBase
    {
        private IGoodsService goodsService;

        private IWebHostEnvironment env;

        public GoodsController(IGoodsService _goodsService, IWebHostEnvironment _env)
        {
            this.goodsService = _goodsService;
            this.env = _env;
        }

        [HttpGet]
        [Route("getlist/{page}/{limit}")]
        [Route("getlist/{keywords}/{page}/{limit}")]
        public IActionResult Test(string keywords,int page, int limit)
        {

            return Ok();
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
        //[Route("/api/test")]
        public IActionResult GetAll()
        {
            return new JsonResult(goodsService.GetAll());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> UploadFileAsync(IFormFile file)
        {
            string extName = Path.GetExtension(file.FileName);
            string fileName = $"{Guid.NewGuid()}{extName}";
            string filePath = Path.Combine(env.WebRootPath, fileName);
            using (var stream = System.IO.File.Create(filePath))
            {
                await file.CopyToAsync(stream);
                await stream.FlushAsync();
            }

            return Ok(new { file = fileName });
        }
    }
}
