using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace curmudgeon.Models
{
    public class EFCurmudgeonRepository : ICurmudgeonRepository
    {

        CurmudgeonDbContext db = new CurmudgeonDbContext();

        public EFCurmudgeonRepository(CurmudgeonDbContext connection = null)
        {
            if (connection == null)
            {
                this.db = new CurmudgeonDbContext();
            }
            else
            {
                this.db = connection;
            }
        }

        public IQueryable<Comment> Comments
        {
            get
            {
                return db.Comments;
            }
        }

        public IQueryable<Post> Posts
        {
            get
            {
                return db.Posts;
            }
        }

        public IQueryable<ApplicationUser> Users
        {
            get
            {
                return db.Users;
            }
        }

        public Comment CommentEdit(Comment comment)
        {
            db.Entry(comment).State = EntityState.Modified;
            db.SaveChanges();
            return comment;
        }

        public void CommentRemove(Comment comment)
        {
            db.Comments.Remove(comment);
            db.SaveChanges();
        }

        public Comment CommentSave(Comment comment, ApplicationUser user)
        {
            comment.User = user;
            db.Comments.Add(comment);
            db.SaveChanges();
            return comment;
        }

        public Post PostEdit(Post post)
        {
            db.Entry(post).State = EntityState.Modified;
            db.SaveChanges();
            return post;
        }

        public void PostRemove(Post post)
        {
            db.Posts.Remove(post);
            db.SaveChanges();
        }

        public Post PostSave(Post post)
        {
            db.Posts.Add(post);
            db.SaveChanges();
            return post;
        }

        public ApplicationUser UserCreate(ApplicationUser user)
        {
            db.Users.Add(user);
            db.SaveChanges();
            return (user);
        }

        public void UserDelete(ApplicationUser user)
        {
            db.Users.Remove(user);
            db.SaveChanges();
        }

        public ApplicationUser UserUpdate(ApplicationUser user)
        {
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
            return user;
        }
    }
}
