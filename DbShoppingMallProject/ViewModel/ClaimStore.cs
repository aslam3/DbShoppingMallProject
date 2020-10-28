using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DbShoppingMallProject.ViewModel
{
    public class ClaimStore
    {
        public static List<Claim> GetClaims = new List<Claim>
        {
            new Claim("Create","Create"),
            new Claim("Read","Read"),
            new Claim("Update","Update"),
            new Claim("Delete","Delete")
        };
    }
}
