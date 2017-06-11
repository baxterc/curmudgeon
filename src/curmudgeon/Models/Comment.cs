using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace curmudgeon.Models
{
    [Table("Comments")]
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public virtual Post CommentPost { get; set; }

        [ForeignKey("CommentPost")]
        public int CommentPostId { get; set; }
        public DateTime CommentDate { get; set; }
        public virtual ApplicationUser User { get; set; }
        public int? ParentCommentId { get; set; }
        [ForeignKey("ParentCommentId")]
        public virtual Comment ParentComment { get; set; }
        [InverseProperty("ParentComment")]
        public virtual ICollection<Comment> ChildComments { get; set; }

        
    }
}
