using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MSM.MVC.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MSM.IService;

namespace MSM.MVC.Controllers
{
    public class HomeController : Controller
    {
        private IGoodsService goodsService;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IGoodsService _goodsService)
        {
            _logger = logger;
            this.goodsService = _goodsService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult vue()
        {
            return View();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Goods()
        {
            return Json(goodsService.GetAll());
        }
    }
}
