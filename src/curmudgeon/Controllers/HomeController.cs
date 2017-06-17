using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using curmudgeon.Models;
using curmudgeon.ViewModels;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using curmudgeon.Utilities;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace curmudgeon.Controllers
{
    public class HomeController : Controller
    {
        private readonly CurmudgeonDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public HomeController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, CurmudgeonDbContext db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _db = db;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel model)
        {
            Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: true, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }
        public async Task<IActionResult> Blogs(string id, int? page)
        {

            if (id == null)
            {
                //Takes the user to their own posts
                var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var thisUser = _db.Users.Where(u => u.Id == userId).Include(u => u.UserPosts).FirstOrDefault();
                Paginator paginator = new Paginator(thisUser.UserPosts.Count, page, 10);
                var paginatedPosts = thisUser.UserPosts.Skip((paginator.CurrentPage - 1) * paginator.PageLength).Take(paginator.PageLength).OrderBy(p => p.Date);
                var viewModel = UserBlogsViewModel.UserConvertBlogViewModel(thisUser, paginatedPosts, paginator);
                return View(viewModel);
            }
            else
            {
                ApplicationUser foundUser = _db.Users.Where(u => u.Nickname == id.ToLower()).Include(u => u.UserPosts).FirstOrDefault();

                var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (foundUser.Id != userId)
                {
                    foundUser.UserPosts = _db.Posts.Where(p => p.Account == foundUser).Where(p => p.Private == false).ToList();
                }

                Paginator paginator = new Paginator(foundUser.UserPosts.Count, page, 10);
                var paginatedPosts = foundUser.UserPosts.Skip((paginator.CurrentPage - 1) * paginator.PageLength).Take(paginator.PageLength).OrderBy(p => p.Date);
                var viewModel = UserBlogsViewModel.UserConvertBlogViewModel(foundUser, paginatedPosts, paginator);
                return View(viewModel);
            }
        }
    }
}
