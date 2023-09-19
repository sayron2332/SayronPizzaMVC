using SayronPizzaMVC.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SayronPizzaMVC.Core.Entites.Catogory
{
    public class AppCategory : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
