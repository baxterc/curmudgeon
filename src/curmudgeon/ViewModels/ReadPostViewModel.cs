using curmudgeon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace curmudgeon.ViewModels
{
    public class ReadPostViewModel
    {
        public Post ReadPost { get; set; }
        public List<Tag> ReadPostTags { get; set; }

        public ReadPostViewModel(Post post, List<Tag>tagList)
        {
            this.ReadPost = post;
            this.ReadPostTags = tagList;
        }
    }
}
