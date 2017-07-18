using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using curmudgeon.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using curmudgeon.Utilities;
using curmudgeon.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace curmudgeon.Controllers
{
    public class TagsController : Controller
    {
        private readonly CurmudgeonDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public TagsController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, CurmudgeonDbContext db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task <IActionResult> Read(string id, int? page)
        {
            //Is it necessary to load up the user for this?
            //var clientUserId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            //var clientUser = _db.Users.Where(u => u.Id == clientUserId).Include(u => u.UserPosts).FirstOrDefault();

            var tag = _db.Tags.Where(t => t.Title == id.ToString()).FirstOrDefault();

            List<PostTag> postTags = _db.PostTags.Where(pt => pt.TagId == tag.TagId).Include(p => p.Post).Where(p => p.Post.IsPrivate == false && p.Post.IsDraft == false).ToList();
           
            List<Post> taggedPosts = new List<Post>();
            foreach (PostTag postTag in postTags)
            {
                taggedPosts.Add(postTag.Post);
                /*if (postTag.Post.IsDraft == false && postTag.Post.IsPrivate == false)
                {
                    taggedPosts.Add(postTag.Post);
                }*/
            }
            Paginator paginator = new Paginator(taggedPosts.Count, page, 10);

            var paginatedPosts = taggedPosts.Skip((paginator.CurrentPage - 1) * paginator.PageLength).Take(paginator.PageLength).OrderBy(p => p.PublishDate);

            TagsReadViewModel model = new TagsReadViewModel()
            {
                Posts = paginatedPosts,
                Paginator = paginator,
                TagName = id
            };

            return View(model);
        }
    }
}
