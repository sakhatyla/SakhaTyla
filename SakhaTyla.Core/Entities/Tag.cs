﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SakhaTyla.Core.Entities
{
    public class Tag : BaseEntity
    {
        public Tag(string name)
        {
            Name = name;
        }

        [Required()]
        [StringLength(100)]
        public string Name { get; set; }
                
    }
}
