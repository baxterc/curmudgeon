using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace curmudgeon.Models
{
    [Table("Tags")]
    public class Tag
    {
        
        [Key]
        public int TagId { get; set; }
        public string Title { get; set; }
        public List<PostTag> PostTags { get; set; }

    }
}
