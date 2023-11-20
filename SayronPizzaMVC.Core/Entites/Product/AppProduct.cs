
using SayronPizzaMVC.Core.Entites.Category;
using SayronPizzaMVC.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SayronPizzaMVC.Core.Interfaces;

namespace SayronPizzaMVC.Core.Entites.Product
{
    public class AppProduct : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Img { get; set; }
        public string Size { get; set; }
        public string Price { get; set; }
        public AppCategory AppCategory { get; set; }
        public int AppCategoryId { get; set; }
    
    }
}
