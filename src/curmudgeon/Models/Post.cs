﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace curmudgeon.Models
{
    [Table("Posts")]
    public class Post
    {
        [Key]
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set;}
        [StringLength(64, MinimumLength = 2)]
        [RegularExpression("[a-z.0-9-]")]
        public string Slug { get; set; }
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

        public static string SlugConverter(string slug)
        {
            slug = slug.ToLower();
            //TODO: Handle non-Latin and accented letters

            //Replace any non-lowercase alphanumeric, non-whitespace, non-hyphen character with nothing
            slug = Regex.Replace(slug, @"[^\w\s\p{Pd}]", "", RegexOptions.Compiled);
            //Replace whitespace of any length or type with a hyphen
            slug = Regex.Replace(slug, @"[\s]+", "-", RegexOptions.Compiled);
            //Replace underscores that are not at the end of the string and not followed by the number with a hyphen
            slug = Regex.Replace(slug, @"_^[0-9]+$", "-", RegexOptions.Compiled);
            //Enforce maximum length of 64 chars
            if (slug.Length > 64)
            {
                slug = slug.Substring(0, 64);
            }
            //Remove leading and ending hyphens
            slug = slug.Trim('-');
            return slug;
        }

        public static string GenerateSlug()
        {
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            string newSlug = "";
            Random random = new Random();
            for (int i = 0; i < 6; i++)
            {
                newSlug.Append(chars[random.Next(chars.Length)]);
            }
            return newSlug;
        }
    }
}
