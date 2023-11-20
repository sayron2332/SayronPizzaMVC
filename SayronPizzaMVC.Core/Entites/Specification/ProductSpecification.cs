using Ardalis.Specification;
using SayronPizzaMVC.Core.Entites.Category;
using SayronPizzaMVC.Core.Entites.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SayronPizzaMVC.Core.Entites.Specification
{
    public class ProductSpecification 
    {
        public class GetAllByCategory : Specification<AppProduct>
        {
            public GetAllByCategory(AppCategory category)
            {
                Query.Where(p => p.AppCategoryId == category.Id);
            }
        }
    }
}
