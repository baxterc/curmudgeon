using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using curmudgeon.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace curmudgeon.Controllers
{
    public class PostsController : Controller
    {
        private readonly CurmudgeonDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public PostsController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, CurmudgeonDbContext db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _db = db;
        }
        
        public async Task<IActionResult> Index()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var thisUser = await _userManager.FindByIdAsync(userId);
            var posts = _db.Posts.Where(p => p.Account == thisUser);
            return View(posts);
        }

        public IActionResult Read(int id)
        {
            var thisPost = _db.Posts.FirstOrDefault(p => p.PostId == id);
            return View(thisPost);
        }

        public IActionResult Write()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Write(Post newPost)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var thisUser = await _userManager.FindByIdAsync(userId);
            newPost.Account = thisUser;
            newPost.Date = DateTime.Today;
            _db.Posts.Add(newPost);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var thisPost = _db.Posts.FirstOrDefault(p => p.PostId == id);

            return View(thisPost);
        }

        [HttpPost]
        public IActionResult Edit(Post editPost)
        {
            _db.Entry(editPost).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var thisPost = _db.Posts.FirstOrDefault(p => p.PostId == id);
            return View(thisPost);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var thisPost = _db.Posts.FirstOrDefault(p => p.PostId == id);
            _db.Posts.Remove(thisPost);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
