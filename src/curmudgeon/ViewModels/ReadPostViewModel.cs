using curmudgeon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using curmudgeon.Utilities;

namespace curmudgeon.ViewModels
{
    public class ReadPostViewModel
    {
        public Post ReadPost { get; set; }
        public List<Tag> ReadPostTags { get; set; }
        public IEnumerable<Comment> PostComments { get; set; }
        public Paginator Paginator { get; set; }

        public ReadPostViewModel(Post post, List<Tag>tagList)
        {
            this.ReadPost = post;
            this.ReadPostTags = tagList;
        }

        public ReadPostViewModel()
        {

        }
    }
}
