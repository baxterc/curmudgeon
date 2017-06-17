using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using curmudgeon.Models;
using curmudgeon.Utilities;

namespace curmudgeon.ViewModels
{
    public class UserBlogsViewModel
    {
        public IEnumerable<Post> Posts { get; set; }
        public string Nickname { get; set; }
        public string DisplayName { get; set; }
        public Paginator Paginator { get; set; }

        public static UserBlogsViewModel UserConvertBlogViewModel(ApplicationUser user, IEnumerable<Post> posts, Paginator paginator)
        {
            UserBlogsViewModel viewModel = new UserBlogsViewModel()
            {
                Nickname = user.Nickname,
                DisplayName = user.DisplayName,
                Posts = posts,
                Paginator = paginator
            };
            return viewModel;
        }
    }
}
