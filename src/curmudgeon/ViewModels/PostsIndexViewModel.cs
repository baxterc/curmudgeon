using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using curmudgeon.Models;
using curmudgeon.Utilities;

namespace curmudgeon.ViewModels
{
    public class PostsIndexViewModel
    {
        public IEnumerable<Post> Posts { get; set; }
        public Paginator Paginator { get; set; }

    }
}
