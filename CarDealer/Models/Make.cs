using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CarDealer.Models
{
    public class Make
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Make Name")]
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
    }
}