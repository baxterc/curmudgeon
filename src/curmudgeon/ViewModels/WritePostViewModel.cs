﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using curmudgeon.Models;

namespace curmudgeon.ViewModels
{
    public class WritePostViewModel : Post
    {
        public string TagsString { get; set; }

        public Post WritePostConvert(WritePostViewModel viewModel)
        {
            Post returnPost = new Post();
            returnPost.Account = viewModel.Account;
            returnPost.Comments = viewModel.Comments;
            returnPost.Content = viewModel.Content;
            returnPost.Date = viewModel.Date;
            returnPost.PostId = viewModel.PostId;
            returnPost.PostTags = viewModel.PostTags;
            returnPost.Title = viewModel.Title;
            return returnPost;
        }
    }
}