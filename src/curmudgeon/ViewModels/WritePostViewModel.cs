using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using curmudgeon.Models;

namespace curmudgeon.ViewModels
{
    public class WritePostViewModel : Post
    {
        public string TagsString { get; set; }


        public static Post WritePostConvert(WritePostViewModel viewModel)
        {
            Post returnPost = new Post();
            returnPost.Account = viewModel.Account;
            returnPost.Comments = viewModel.Comments;
            returnPost.Content = viewModel.Content;
            returnPost.PublishDate = viewModel.PublishDate;
            returnPost.PostId = viewModel.PostId;
            returnPost.PostTags = viewModel.PostTags;
            returnPost.Slug = viewModel.Slug;
            returnPost.Title = viewModel.Title;
            returnPost.IsPrivate = viewModel.IsPrivate;
            return returnPost;
        }

        public WritePostViewModel()
        {

        }

        public WritePostViewModel(Post post, string tagString)
        {
            this.Account = post.Account;
            this.Comments = post.Comments;
            this.Content = post.Content;
            this.PublishDate = post.PublishDate;
            this.PostId = post.PostId;
            this.PostTags = post.PostTags;
            this.Slug = post.Slug;
            this.Title = post.Title;
            this.TagsString = tagString;
        }
    }
}
