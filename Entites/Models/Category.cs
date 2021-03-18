﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entites.Models
{
    public class Category
    {
        public int CategoryID { get; set; }

        [Required(ErrorMessage = "Category name is required")]
        [StringLength(15, ErrorMessage = "Category name can't be longer than 15 charachters")]
        public string CategoryName { get; set; }

        public string Description { get; set; }

        public byte[] Picture { get; set; }

        // Nav
        public ICollection<Product> Products { get; set; }
    }
}
