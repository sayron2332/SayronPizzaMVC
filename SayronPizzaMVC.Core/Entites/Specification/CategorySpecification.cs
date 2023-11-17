using Ardalis.Specification;
using SayronPizzaMVC.Core.Entites.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace SayronPizzaMVC.Core.Entites.Specification
{
    public class CategorySpecification
    {
        public class GetByName : Specification<AppCategory>
        {
            public GetByName(string name)
            {
                Query.Where(x => x.Name == name);
            }
        }
    }
}
