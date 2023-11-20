using SayronPizzaMVC.Core.Entites.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SayronPizzaMVC.Core.DTO_s.Products
{
    public class ProductDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Img { get; set; }
        public string Size { get; set; }
        public string Price { get; set; }
        public string AppCategoryName { get; set; }
    }
}
