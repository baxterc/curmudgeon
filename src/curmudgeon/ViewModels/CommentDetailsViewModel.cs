using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using curmudgeon.Models;

namespace curmudgeon.ViewModels
{
    public class CommentDetailsViewModel
    {
        public Comment ParentComment { get; set; }
        public Comment ReplyComment { get; set; }
    }
}
