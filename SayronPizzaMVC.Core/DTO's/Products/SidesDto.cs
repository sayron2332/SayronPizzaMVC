using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SayronPizzaMVC.Core.DTO_s.Products
{
    public class SidesDto
    {
        public string? ImagePath { get; set; } = "Default.png";
        public string Name { get; set; }

        public string Size { get; set; }

        public string Price { get; set; }       
    }
}
