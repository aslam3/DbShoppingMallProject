using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DbShoppingMallProject.Data;
using DbShoppingMallProject.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DbShoppingMallProject.Controllers
{
    public class ProfileController : Controller
    {
        private UserManager<ApplicationUser> userManager;
        private readonly IHostingEnvironment hosting;

        public ProfileController(UserManager<ApplicationUser> userManager, IHostingEnvironment hosting)
        {
            this.userManager = userManager;
            this.hosting = hosting;
        }
        public IActionResult Index()
        {
            var userID = userManager.GetUserId(HttpContext.User);
            ApplicationUser user = userManager.FindByIdAsync(userID).Result;

            return View(user);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var userID = userManager.GetUserId(HttpContext.User);
            ApplicationUser user = userManager.FindByIdAsync(userID).Result;

            var user1 = await userManager.FindByIdAsync(id);
            ProfileVm vm = new ProfileVm
            {
                Id = id,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                ImagePath = user.ImagePath,
                SecurityStamp = user.SecurityStamp

            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProfileVm vm)
        {
            try
            {
                var user1 = await userManager.FindByIdAsync(vm.Id);
                if (user1 == null)
                {
                    ViewBag.ErrorMessage = $"User with Id={vm.Id} cannot be found";
                    return View("NotFound");
                }
                else
                {
                    if (vm.Imgfile != null)
                    {
                        string ext = Path.GetExtension(vm.Imgfile.FileName).ToLower();
                        if (ext == ".jpg" || ext == ".png" || ext == ".jpeg")
                        {
                            var filename = Path.GetFileNameWithoutExtension(vm.Imgfile.FileName) + "_ "+ Guid.NewGuid() + ext;
                            var filePath = Path.Combine(hosting.WebRootPath, "images\\ProfilePic", filename);
                            using (var fileStream = new FileStream(filePath, FileMode.Create))
                            {
                                vm.Imgfile.CopyTo(fileStream);
                                user1.ImagePath = "\\images\\ProfilePic\\" + filename;
                            }
                        }
                    }
                    else
                    {
                        user1.ImagePath = vm.ImagePath;
                    }

                    user1.Id = vm.Id;
                    user1.UserName = vm.UserName;
                    user1.Email = vm.Email;
                   
                    user1.SecurityStamp = vm.SecurityStamp;
                    user1.PhoneNumber = vm.PhoneNumber;


                    IdentityResult result = await userManager.UpdateAsync(user1);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    foreach (var e in result.Errors)
                    {
                        ModelState.AddModelError("", e.Description);
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Result = ex.Message;
            }

            return View(vm);
        }
    }
}