using Microsoft.AspNetCore.Http;
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
        private string? _imagePath;
        public string? ImagePath
        {
            get => _imagePath;
            set => _imagePath = value ?? defaultPath;
        }
        const string defaultPath = "Default.png";
        public int AppCategoryId { get; set; }
        public string AppCategoryName { get; set; } = string.Empty;
        public IFormFileCollection File { get; set; }
        public string Slug => Name?
            .Replace("а", "a")
            .Replace("б", "b")
            .Replace("в", "v")
            .Replace("г", "g")
            .Replace("д", "d")
            .Replace("е", "e")
            .Replace("є", "e")
            .Replace("ё", "e")
            .Replace("ж", "j")
            .Replace("з", "z")
            .Replace("и", "i")
            .Replace("ї", "yi")
            .Replace("й", "i")
            .Replace("к", "k")
            .Replace("л", "l")
            .Replace("м", "m")
            .Replace("н", "n")
            .Replace("о", "o")
            .Replace("п", "p")
            .Replace("р", "r")
            .Replace("с", "s")
            .Replace("т", "t")
            .Replace("у", "u")
            .Replace("ф", "f")
            .Replace("х", "h")
            .Replace("ц", "c")
            .Replace("ч", "ch")
            .Replace("ш", "sh")
            .Replace("щ", "shch")
            .Replace("ы", "y")
            .Replace("э", "e")
            .Replace("ю", "u")
            .Replace("я", "ya")
            .Replace(":", "-")
            .Replace(" ", "-").ToLower().ToString() + ".html";
    
}
}
