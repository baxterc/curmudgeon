using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using curmudgeon.Models;

namespace curmudgeon.ViewModels
{
    public class EditPostViewModel
    {
        public Post EditPost { get; set; } 
        public string TagsString { get; set; }

        public EditPostViewModel()
        {

        }

        /*
        public EditPostViewModel(Post editPost, String tagsString)
        {
            this.EditPost = editPost;
            this.TagsString = tagsString;
        }
        */
    }
}
