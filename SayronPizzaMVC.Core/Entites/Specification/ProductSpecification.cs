using Ardalis.Specification;
using Microsoft.Extensions.Hosting;
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
        public class GetAllByCategoryName : Specification<AppProduct>
        {
            public GetAllByCategoryName(string name)
            {
                Query.Where(p => p.AppCategory.Name == name);
            }
        }
        public class All : Specification<AppProduct>
        {
            public All()
            {
                Query.Include(x => x.AppCategory);
            }
        }
        public class ByCategory : Specification<AppProduct>
        {
            public ByCategory(int categoryId)
            {
                Query
                  .Include(x => x.AppCategory)
                  .Where(c => c.AppCategoryId == categoryId).OrderByDescending(x => x.Id); ;
            }
        }
        public class Search : Specification<AppProduct>
        {
            public Search(string searchString)
            {
                Query
                    .Include(p => p.AppCategory)
                    .Where(p => p.Name.Contains(searchString)).OrderByDescending(p => p.Id);
            }
        }
    }
}
