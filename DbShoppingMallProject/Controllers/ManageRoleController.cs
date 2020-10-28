using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DbShoppingMallProject.Data;
using DbShoppingMallProject.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DbShoppingMallProject.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class ManageRoleController : Controller
    {
        
        private ApplicationDbContext db { get; set; }
        public readonly RoleManager<IdentityRole> manager;
        public readonly UserManager<ApplicationUser> userManager;
        public ManageRoleController(RoleManager<IdentityRole> role, UserManager<ApplicationUser> userManager)
        {
            this.manager = role;
            this.userManager = userManager;
        }
        public IActionResult Index()
        {
            return View(manager.Roles.ToList());
        }
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(RoleVm vm)
        {
            IdentityRole identityRole = new IdentityRole
            {
                Name = vm.Name,
            };
            if (ModelState.IsValid)
            {
                IdentityResult result = await manager.CreateAsync(identityRole);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                if (result.Errors.Count() > 0)
                {
                    foreach (var s in result.Errors)
                    {
                        ModelState.AddModelError("", s.ToString());
                    }
                }
            }
            return View(identityRole);
        }


        public IActionResult AssignRole()
        {
            try
            {
                var list = userManager.Users.ToList();
                ViewBag.Userlist = new SelectList(list, "Id", "UserName");
                ViewBag.Rolelist = new SelectList(manager.Roles, "Name", "Name");

            }
            catch (Exception ex)
            {
                ViewBag.result = ex.Message;
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AssignRole(string Userlist, string Rolelist)
        {
            try
            {
                var list = userManager.Users.ToList();
                ViewBag.Userlist = new SelectList(list, "Id", "UserName");
                ViewBag.Rolelist = new SelectList(manager.Roles, "Name", "Name");
                var user = await userManager.FindByIdAsync(Userlist);
                IdentityResult result = await userManager.AddToRoleAsync(user, Rolelist);
                if (result.Succeeded)
                {
                    ViewBag.result = user.UserName + "successfully assign" + Rolelist;
                }
                if (result.Errors.Count() > 0)
                {

                }
            }
            catch (Exception ex)
            {
                ViewBag.result = ex.Message;
            }




            return View("AssignRole");
        }


        [Authorize(Roles ="it")]
        public async Task<IActionResult> DisplayClaim()
        {
            var claimVm = new ClaimVm
            {
                Email = ""
            };
            try
            {
                var list = userManager.Users.ToList();
                ViewBag.Userlist = new SelectList(list, "UserName", "UserName");
                var allClaims = ClaimStore.GetClaims;

                var user = await userManager.FindByEmailAsync(claimVm.Email);
                if (user == null)
                {
                    foreach (var c in allClaims)
                    {
                        UserClaims userClaims = new UserClaims
                        {
                            ClaimType = c.Type,
                            Isselected = false
                        };

                        claimVm.UserClaims.Add(userClaims);
                    }
                }
                else
                {
                    var existingClaim = await userManager.GetClaimsAsync(user);
                    foreach (var c in allClaims)
                    {
                        UserClaims userClaims = new UserClaims
                        {
                            ClaimType = c.Type,
                        };
                        if (existingClaim.Any(a => a.Type == c.Type))
                        {
                            userClaims.Isselected = true;
                        }
                        claimVm.UserClaims.Add(userClaims);
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Result = ex.Message;
            }
            return View(claimVm);
        }
        [HttpPost]
        public async Task<IActionResult> DisplayClaim(ClaimVm vm, string Userlist)
        {
            var user = await userManager.FindByEmailAsync(Userlist);
            var existingClaim = await userManager.GetClaimsAsync(user);
            foreach (var c in existingClaim)
            {
                await userManager.RemoveClaimAsync(user, c);
            }
            foreach (var c in vm.UserClaims)
            {
                if (c.Isselected)
                {
                    IdentityResult result = await userManager.AddClaimAsync(user, new System.Security.Claims.Claim(c.ClaimType, c.ClaimType));
                    if (result.Succeeded)
                    {
                        ViewBag.Result = "Selected claim on" + vm.Email + " successfully assigned";
                    }
                }
            }
            return View(vm);
        }
    }
}