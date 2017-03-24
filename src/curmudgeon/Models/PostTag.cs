using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace curmudgeon.Models
{
    public class PostTag
    {
        [Key]
        public int PostTagId { get; set; }
        [ForeignKey("PostId")]
        public int PostId { get; set; }
        public Post Post { get; set; }
        [ForeignKey("TagId")]
        public int TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
