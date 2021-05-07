using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSM.Model.Entity
{
    public class MsmUser: IdentityUser<long>
    {
        #region 扩展自定义属性
        public string Province { get; set; }
        public string City { get; set; }
        public string Area { get; set; }
        #endregion
    }
}
