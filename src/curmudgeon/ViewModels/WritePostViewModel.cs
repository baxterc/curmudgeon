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
            returnPost.Date = viewModel.Date;
            returnPost.PostId = viewModel.PostId;
            returnPost.PostTags = viewModel.PostTags;
            returnPost.Title = viewModel.Title;
            returnPost.Private = viewModel.Private;
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
            this.Date = post.Date;
            this.PostId = post.PostId;
            this.PostTags = post.PostTags;
            this.Title = post.Title;
            this.TagsString = tagString;
        }
    }
}
