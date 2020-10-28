using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DbShoppingMallProject.ViewModel
{
    public class ClaimVm
    {
        public ClaimVm()
        {
            UserClaims = new List<UserClaims>();
        }
        public string Email { get; set; }
        public List<UserClaims> UserClaims { get; set; }
    }
}
