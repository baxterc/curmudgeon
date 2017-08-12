using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using curmudgeon.Models;
using curmudgeon.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Moq;

namespace curmudgeon.Controllers
{
    public class CommentsController : Controller
    {
        private readonly CurmudgeonDbContext _db;
        private readonly ICurmudgeonRepository _ctx;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public static void CommentDelete (int commentId, CurmudgeonDbContext db)
        {
            Comment deleteComment = db.Comments.Where(c => c.CommentId == commentId).Include(c => c.ChildComments).FirstOrDefault();

            db.Comments.Remove(deleteComment);

            if (deleteComment.ChildComments != null)
            {
                   foreach (Comment reply in deleteComment.ChildComments)
                {
                    CommentDelete(reply.CommentId, db);
                }
            }
        }

        public CommentsController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, CurmudgeonDbContext db, ICurmudgeonRepository ctx = null)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _db = db;
        }

        public async Task <IActionResult> Index()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var thisUser = await _userManager.FindByIdAsync(userId);
            var comments = _db.Comments.Where(c => c.User == thisUser);
            return View(comments); ;
        }

        public IActionResult Write(int id)
        {
            TempData["postId"] = id;
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Write(Comment newComment)
        {
            var postId = int.Parse(TempData["postId"].ToString());
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var thisUser = await _userManager.FindByIdAsync(userId);
            newComment.User = thisUser;
            newComment.CommentDate = DateTime.Today;
            newComment.CommentPostId = postId;
            _db.Comments.Add(newComment);
            _db.SaveChanges();
            return RedirectToAction("Read", "Posts", new { id = postId });
        }

        public IActionResult Details(int id)
        {
            var thisComment = _db.Comments.Where(c => c.CommentId == id).Include(c => c.ChildComments).FirstOrDefault();
            CommentDetailsViewModel model = new CommentDetailsViewModel()
            {
                ParentComment = thisComment
            };
            
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Details(CommentDetailsViewModel commentModel)
        {
            Comment parentComment = _db.Comments.FirstOrDefault(p => p.CommentId == commentModel.ParentComment.CommentId);

            Comment childComment = commentModel.ReplyComment;
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var thisUser = await _userManager.FindByIdAsync(userId);
            childComment.CommentPostId = parentComment.CommentPostId;
            childComment.CommentDate = DateTime.Today;

            childComment.ParentComment = parentComment;
            childComment.ParentCommentId = parentComment.CommentId;
            childComment.CommentPost = parentComment.CommentPost;

            _db.Comments.Add(childComment);

            _db.SaveChanges();

            return RedirectToAction("Read", "Posts", new { id = parentComment.CommentPostId});
        }

        public IActionResult Update(int id)
        {
            var thisComment = _db.Comments.FirstOrDefault(c => c.CommentId == id);
            return View(thisComment);
        }

        [HttpPost]
        public IActionResult Edit(Comment editComment)
        {
            _db.Entry(editComment).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        { 
            var thisComment = _db.Comments.FirstOrDefault(c => c.CommentId == id);
            TempData["postId"] = thisComment.CommentPostId;
            return View(thisComment);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var postId = int.Parse(TempData["postId"].ToString());
            /*
            
            var thisComment = _db.Comments.Where(c => c.CommentId == id).Include(c => c.ChildComments).FirstOrDefault();
            
            _db.Comments.Remove(thisComment);
            _db.SaveChanges();

            */
            
            CommentDelete(id, _db);

            _db.SaveChanges();

            return RedirectToAction("Read", "Posts", new { id = postId });
        }
    }
}
