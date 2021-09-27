using System;
using System.Collections.Generic;
using System.Text;

namespace MSM.Model.Model
{
    public class ResultInfo
    {
        /// <summary>
        /// 返回消息
        /// </summary>
        public string msg { get; set; }
        /// <summary>
        /// 状态码：0-成功，>0-失败
        /// </summary>
        public int code { get; set; }
        /// <summary>
        /// JWT令牌
        /// </summary>
        public string token { get; set; }
        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime exp { get; set; }
        public int status { get; set; }
    }
}
