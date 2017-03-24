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
        public int PostId { get; set; }
        public virtual Post CommentPost { get; set; }
        public virtual ApplicationUser User { get; set; }
        
        
    }
}
