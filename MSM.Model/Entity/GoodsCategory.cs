using System;
using System.Collections.Generic;
using System.Text;

namespace MSM.Model.Entity
{
    public class GoodsCategory
    {
        /// <summary>
        /// 分类ID
        /// </summary>
        public int CategoryID { get; set; }
        /// <summary>
        /// 分类名称
        /// </summary>
        public string CategoryName { get; set; }
        /// <summary>
        /// 父ID
        /// </summary>
        public int ParentID { get; set; }
        /// <summary>
        /// 分类级别
        /// </summary>
        public int Depth { get; set; }
        /// <summary>
        /// 分类路径
        /// </summary>
        public string ParentPath { get; set; }
        /// <summary>
        /// 导航属性
        /// </summary>
        public List<Goods> Goods { get; set; }
    }
}
