using System;
using System.Collections.Generic;
using System.Text;

namespace MSM.Model.Model
{
    public class GoodsModel
    {
        /// <summary>
        /// 商品ID
        /// </summary>
        public int GoodsID { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string GoodsName { get; set; }
        /// <summary>
        /// 商品图片
        /// </summary>
        public string GoodsPic { get; set; }
        /// <summary>
        /// 商品价格
        /// </summary>
        public decimal GoodsMoney { get; set; }
        /// <summary>
        /// 所属分类
        /// </summary>
        public int CategoryID { get; set; }
        /// <summary>
        /// 导航属性
        /// </summary>
    }
}
