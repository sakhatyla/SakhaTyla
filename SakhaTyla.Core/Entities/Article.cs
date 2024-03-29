﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SakhaTyla.Core.Entities
{
    public class Article : TrackedEntity
    {
        public Article(string title, string textSource)
        {
            Title = title;
            TextSource = textSource;
        }

        [Required()]
        [StringLength(200)]
        public string Title { get; set; }
        
        [Required()]
        public string Text { get; set; } = null!;

        [Required()]
        public string TextSource { get; set; }
        
        [Required()]
        public int FromLanguageId { get; set; }
        public Language FromLanguage { get; set; } = null!;
        
        [Required()]
        public int ToLanguageId { get; set; }
        public Language ToLanguage { get; set; } = null!;
        
        [Required()]
        public bool IsDeleted { get; set; }
        
        [Required()]
        public bool Fuzzy { get; set; }
        

        public int? CategoryId { get; set; }
        public Category? Category { get; set; }

        public ICollection<ArticleTag> Tags { get; set; } = null!;

    }
}
