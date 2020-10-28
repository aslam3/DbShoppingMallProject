using DbShoppingMallProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DbShoppingMallProject.ViewModel
{
    public class CatStallVm
    {
        public string Category { get; set; }
        public string Description { get; set; }
        public IList<Stall> Stalls { get; set; }
    }
}
