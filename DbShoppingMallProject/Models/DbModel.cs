using DbShoppingMallProject.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DbShoppingMallProject.Models
{
    public class DbModel
    {
    }
    public class Stall
    {
        public int Id { get; set; }
        public string StallNo { get; set; }
        public string StallName { get; set; }
        [ForeignKey("Category")]
        public int CatId { get; set; }
        public int StallVolume { get; set; }
        
        public Level Level { get; set; }
        public virtual Category Category { get; set; }
        public ICollection<StallManager> StallManagers { get; set; }
       
    }

    public enum Level
    {
        [Display(Name = @"1st Floor")]
        First = 0,
        [Display(Name = @"2nd Floor")]
        Secoend = 1,
        [Display(Name = @"3rd Floor")]
        Third = 2,
        [Display(Name = @"4th Floor")]
        Four = 3,
        [Display(Name = @"5th Floor")]
        Five = 4,
        [Display(Name =@"6th Floor")]
        Sixth=5
            
    }
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Stall> stalls { get; set; }
    }
}
