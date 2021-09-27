using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MSM.MVC.Models;

namespace MSM.MVC.Controllers
{
    public class DefaultController : Controller
    {
        private StudentDAL StudentDAL;
        public DefaultController(StudentDAL _StudentDAL)
        {
            this.StudentDAL = _StudentDAL;
        }


        public IActionResult Index()
        {
            return Ok(StudentDAL.test());
        }
    }
}
