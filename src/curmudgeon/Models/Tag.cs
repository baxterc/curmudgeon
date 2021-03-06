﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace curmudgeon.Models
{
    [Table("Tags")]
    public class Tag
    {
        
        [Key]
        public int TagId { get; set; }
        public string Title { get; set; }
        public virtual List<PostTag> PostTags { get; set; }

        public Tag(string title)
        {
            Title = title;
        }

        public Tag()
        {

        }

        public static string Sluggify(string slug)
        {
            slug = slug.ToLower();
            //TODO: Handle non-Latin and accented letters

            //Replace any non-lowercase alphanumeric, non-whitespace, non-hyphen character with nothing
            slug = Regex.Replace(slug, @"[^\w\s\p{Pd}]", "", RegexOptions.Compiled);
            //Replace whitespace and underscores of any length or type with a hyphen
            slug = Regex.Replace(slug, @"[\s_]+", "-", RegexOptions.Compiled);
            //Replace underscores that are not at the end of the string and not followed by a number with a hyphen -- deprecated
            //slug = Regex.Replace(slug, @"_^[0-9]+$", "-", RegexOptions.Compiled);
            //Enforce maximum length of 32 chars
            if (slug.Length > 32)
            {
                slug = slug.Substring(0, 32);
            }
            //Remove leading and ending hyphens
            slug = slug.Trim('-');

            //If the slug is null or all hyphens, return null
            if (slug == null || slug == "")
            {
                return null;
            }
            else if (Regex.IsMatch(slug, @"^-*$"))
            {
                return null;
            }
            return slug;
        }
    }
}
