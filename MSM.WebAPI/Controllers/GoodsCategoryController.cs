using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MSM.Model.Model;
using MSM.IService;

namespace MSM.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GoodsCategoryController : ControllerBase
    {
        private IGoodsCategoryService goodsCategoryService;

        public GoodsCategoryController(IGoodsCategoryService _goodsCategoryService)
        {
            this.goodsCategoryService = _goodsCategoryService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return new JsonResult(await goodsCategoryService.getAllData());
        }
    }
}
