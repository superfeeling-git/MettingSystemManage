using System;
using System.Collections.Generic;
using System.Text;

namespace MSM.Model.Model
{
    public class CategoryModel
    {
        public int value { get; set; }
        public string label { get; set; }
        public List<CategoryModel> children { get; set; } = new List<CategoryModel>();
    }
}
