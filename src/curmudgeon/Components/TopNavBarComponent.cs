using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using curmudgeon.Models;
using Microsoft.AspNetCore.Identity;
using curmudgeon.ViewModels;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace curmudgeon.Components
{
    public class TopNavBarComponent : ViewComponent
    {
        private readonly CurmudgeonDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public TopNavBarComponent(CurmudgeonDbContext db, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId != null)
            { 
                var thisUser = await _userManager.FindByIdAsync(userId);
                var viewModel = new TopNavBarViewModel(thisUser.DisplayName, thisUser.UserColors);
                return View(viewModel);
            }
            else
            {
                return View();
            }
        }
    }
}
