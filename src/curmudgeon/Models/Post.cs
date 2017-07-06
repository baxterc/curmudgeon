using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace curmudgeon.Models
{
    [Table("Posts")]
    public class Post
    {
        [Key]
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime PublishDate { get; set; }
        public bool IsPrivate { get; set; }
        public bool IsDraft { get; set; }
        public DateTime DraftDate { get; set; }
        public virtual ICollection<PostTag> PostTags { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ApplicationUser Account { get; set; } 
        
        public Post(string title, string content, DateTime date)
        {
            Title = title;
            Content = content;
            PublishDate = date;
        }

        public Post()
        {

        }
    }
}
