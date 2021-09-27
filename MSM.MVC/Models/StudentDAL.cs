using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSM.MVC.Models
{
    public abstract class StudentDAL : IStudentDAL
    {
        public StudentDAL()
        {

        }
        public string test()
        {
            return "abc";
        }
    }
}
