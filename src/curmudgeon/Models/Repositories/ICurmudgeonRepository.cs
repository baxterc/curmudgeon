using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace curmudgeon.Models
{
    public interface ICurmudgeonRepository
    {
        IQueryable<Post> Posts { get; }
        Post PostSave(Post post);
        Post PostEdit(Post post);
        void PostRemove(Post post);

        IQueryable<Comment> Comments { get; }
        Comment CommentSave(Comment comment, ApplicationUser user);
        Comment CommentEdit(Comment comment);
        void CommentRemove(Comment comment);

        IQueryable<ApplicationUser> Users { get; }
        ApplicationUser UserCreate(ApplicationUser user);
        ApplicationUser UserUpdate(ApplicationUser user);
        void UserDelete(ApplicationUser user);

    }
}

