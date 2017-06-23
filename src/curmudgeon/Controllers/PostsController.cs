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
using curmudgeon.Utilities;

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
        
        public async Task<IActionResult> Index(int? page)
        {

            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var thisUser = await _userManager.FindByIdAsync(userId);
            var posts = _db.Posts.Where(p => p.Account == thisUser).ToList();

            Paginator paginator = new Paginator(posts.Count, page, 10);

            var paginatedPosts = posts.Skip((paginator.CurrentPage - 1) * paginator.PageLength).Take(paginator.PageLength).OrderBy(p => p.Date);

            PostsIndexViewModel model = new PostsIndexViewModel()
            {
                Posts = paginatedPosts,
                Paginator = paginator
            };

            return View(model);
        }

        public IActionResult Read(int id, int? commentpage)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var thisPost = _db.Posts
                .Where(p => p.PostId == id)
                .Include(p => p.Comments)
                .Include(p => p.PostTags)
                .Include(p => p.Account)
                .FirstOrDefault();

            if (thisPost.Account.Id != userId && thisPost.Private == true)
            {
                Post privatePost = new Post()
                {
                    Title = "This post is marked private",
                    Content = "The author of this post post has made this post private and it cannot be viewed.",
                    Private = true
                };
                ReadPostViewModel privatePostViewModel = new ReadPostViewModel()
                {
                    ReadPost = privatePost
                };
                return View(privatePostViewModel);
            }

            List<Tag> thisPostTags = new List<Tag>();
            string thisPostTagsString = "";

            if (thisPost.PostTags != null)
            {
                foreach (PostTag postTag in thisPost.PostTags)
                {
                    var addTag = _db.Tags
                        .Where(t => t.TagId == postTag.TagId)
                        .FirstOrDefault();
                    thisPostTags.Add(addTag);
                    thisPostTagsString += addTag.Title + ",";
                }
            }

            Paginator paginator = new Paginator(thisPost.Comments.Count , commentpage, 10);

            var paginatedComments = thisPost.Comments.Skip((paginator.CurrentPage - 1) * paginator.PageLength).Take(paginator.PageLength);

            ReadPostViewModel readPost = new ReadPostViewModel(thisPost, thisPostTags);
            readPost.PostComments = paginatedComments;
            readPost.Paginator = paginator;

            return View(readPost);
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
            Post savePost = WritePostViewModel.WritePostConvert(newPost);
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
            var thisPost = _db.Posts
                           .Where(p => p.PostId == id)
                           .Include(p => p.PostTags)
                           .FirstOrDefault();

            List<Tag> thisPostTags = new List<Tag>();
            string thisPostTagsString = "";

            foreach (PostTag postTag in thisPost.PostTags)
            {
                var addTag = _db.Tags
                    .Where(t => t.TagId == postTag.TagId)
                    .FirstOrDefault();
                thisPostTags.Add(addTag);
                thisPostTagsString += addTag.Title.ToString() + ",";
            }

            if (thisPostTagsString.Length > 0)
            {
                thisPostTagsString = thisPostTagsString.Substring(0, (thisPostTagsString.Length - 1));
            }

            WritePostViewModel editPost = new WritePostViewModel(thisPost, thisPostTagsString);

            TempData["PostTagsString"] = thisPostTagsString;
            TempData.Keep("PostTagsString");

            return View(editPost);
        }

        [HttpPost]
        public IActionResult Edit(WritePostViewModel editPost)
        {
            
            string initialTagsString = "";
            if (initialTagsString != null)
            {
                initialTagsString = TempData.Peek("PostTagsString").ToString();
            }

            // Converts the viewmodel's post content into a Post object that can be saved later on.
            Post savePost = WritePostViewModel.WritePostConvert(editPost);

            //Check to see if the tags string assembled from the initial post is different from what was submitted

            if (editPost.TagsString.ToString() != initialTagsString)
            {
                //PostTags associated with the entry
                var editPostPostTags = _db.PostTags.Where(pt => pt.PostId == editPost.PostId).Include(pt => pt.Tag).ToList();

                List<Tag> TagsForThisPost = new List<Tag>();

                foreach (PostTag postTag in editPostPostTags)
                {
                    // Populates a list of actual Tags that are associated with this post
                    TagsForThisPost.Add(postTag.Tag);
                }

                //PostTags associated with the user's input
                string tagsString = editPost.TagsString;
                List<string> tagsSplitString = new List<string>();
                if (tagsString != null)
                {
                    tagsSplitString = tagsString.Split(',').ToList();
                }

                List<Tag> FoundTags = new List<Tag>();

                foreach (string tagString in tagsSplitString)
                {
                    Tag foundTag = _db.Tags.Where(t => t.Title == tagString).FirstOrDefault();
                    //Add the tag to the DB if it does not already exist; if it doesn't exist it's implied that a new PostTag entry should be made
                    if (foundTag == null)
                    {
                        PostTag newPostTag = new PostTag();
                        Tag newTag = new Tag(tagString);
                        _db.Tags.Add(newTag);
                        newPostTag.PostId = savePost.PostId;
                        newPostTag.TagId = newTag.TagId;
                        _db.PostTags.Add(newPostTag);
                    }
                    //If the tag already exists...
                    else
                    {
                        //...Make a new PostTag entry if there isn't already one. i.e. if the PostTags for this post do not contain an entry with this tag ID, make a new entry
                        if (!TagsForThisPost.Contains(foundTag))
                        {
                            PostTag newPostTag = new PostTag();
                            newPostTag.Post = savePost;
                            newPostTag.Tag = foundTag;
                            _db.PostTags.Add(newPostTag);
                        }
                        // loop breaks if the tag already exists and is found in the post's tags.
                    }
                }

                //For each tag in this post...
                foreach (Tag tag in TagsForThisPost)
                {
                    //...If the Tag's title is not found in the array of tag titles or if the tag string is null...
                    if (!tagsSplitString.Contains(tag.Title) || tagsString == null)
                    {
                        //...Delete the PostTag entry between this post and this tag
                        _db.PostTags.Remove(tag.PostTags.Where(pt => pt.TagId == tag.TagId).FirstOrDefault());
                    }
                }
            }

            
            //Save changes to the actual post
            _db.Entry(savePost).State = EntityState.Modified;
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
            var thisPost = _db.Posts
                .Where(p => p.PostId == id)
                .Include(p => p.Comments)
                .Include(p => p.PostTags)
                .FirstOrDefault();
            //Delete the PostTag join entries for this particular Post
            //var thisPostTags = _db.PostTags.Where(pt => pt.PostId == id);
            /*
            foreach (PostTag postTag in thisPostTags)
            {
                _db.PostTags.Remove(postTag);
            }
            */
            _db.Comments.RemoveRange(thisPost.Comments);
            _db.PostTags.RemoveRange(thisPost.PostTags);
            _db.Posts.Remove(thisPost);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
