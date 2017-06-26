using curmudgeon.Models;
using curmudgeon.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace curmudgeon.ViewModels
{
    public class TagsReadViewModel
    {
        public IEnumerable<Post> Posts { get; set; }
        public string TagName { get; set; }
        public Paginator Paginator { get; set; }

    }
}
