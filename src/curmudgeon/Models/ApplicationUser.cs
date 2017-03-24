using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace curmudgeon.Models
{
    
    public class ApplicationUser : IdentityUser
    {
        
        public string Nickname { get; set; }
        public string DisplayName { get; set; }
        
        public ICollection<Post> UserPosts { get; set; }
        public ICollection<Comment> UserComments { get; set; }
        [InverseProperty("Follower")]
        public ICollection<UserFollow> Followers { get; set; }
        [InverseProperty("User")]
        public ICollection<UserFollow> Following { get; set; }
        
    }

    public class UserFollow
    {
        [Key]
        public int UserFollowId { get; set; }
        
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
        
        public string FollowerId { get; set; }
        public virtual ApplicationUser Follower { get; set; }
    }
    /*
    public class UserFollowed : ApplicationUser
    {

    }

    public class Follower : ApplicationUser
    {

    }*/
}
