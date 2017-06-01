using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using curmudgeon.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using curmudgeon.ViewModels;

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
            var thisPost = _db.Posts
                .Where(p => p.PostId == id)
                .Include(p => p.Comments)
                .FirstOrDefault();
            //var thisPost = _db.Posts.FirstOrDefault(p => p.PostId == id);
                //Include(p => p.Comments).FirstOrDefault();
            return View(thisPost);
        }

        public IActionResult Write()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Write(WritePostViewModel newPost)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var thisUser = await _userManager.FindByIdAsync(userId);
            newPost.Account = thisUser;
            newPost.Date = DateTime.Today;
            Post savePost = newPost.WritePostConvert(newPost);
            _db.Posts.Add(savePost);
            string tagsString = newPost.TagsString;
            string[] tags = tagsString.Split(',');

            foreach(string s in tags)
            {
                if (_db.Tags.Any(t => t.Title == s))
                    {
                    PostTag newPostTag = new PostTag();
                    Tag foundTag = _db.Tags.FirstOrDefault(t => t.Title == s);
                    newPostTag.PostId = savePost.PostId;
                    newPostTag.TagId = foundTag.TagId;
                    _db.PostTags.Add(newPostTag);
                    Console.WriteLine("DB contains this tag");
                    }
                else
                {
                    PostTag newPostTag = new PostTag();
                    Console.WriteLine("DB does not contain this tag");
                    Tag newTag = new Tag(s);
                    _db.Tags.Add(newTag);
                    newPostTag.PostId = savePost.PostId;
                    newPostTag.TagId = newTag.TagId;
                    _db.PostTags.Add(newPostTag);
                }
            }

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
