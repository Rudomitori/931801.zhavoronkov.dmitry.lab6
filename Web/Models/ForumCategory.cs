using System;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class ForumCategory
    {
        public Guid Id { get; set; } 

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}