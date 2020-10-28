using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DbShoppingMallProject.ViewModel
{
    public class ProfileVm
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string SecurityStamp { get; set; }
        public string PhoneNumber { get; set; }
        public string ImagePath { get; set; }
        [NotMapped]
        public IFormFile Imgfile { get; set; }
        public string NormalizedUserName { get; set; }
        public string NormalizedEmail { get; set; }
        public string PasswordHash { get; set; }
        public DateTime LockoutEnd { get; set; }
        public string ConcurrencyStamp { get; set; }
        public DateTime Dob { get; set; }
        public int AccessFailedCount { get; set; }
    }
}
