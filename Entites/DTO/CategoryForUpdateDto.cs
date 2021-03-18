using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entites.DTO
{
    public class CategoryForUpdateDto
    {
        [Required(ErrorMessage = "Category name is required")]
        [StringLength(15, ErrorMessage = "Category name can't be longer than 15 charachters")]
        public string CategoryName { get; set; }

        public string Description { get; set; }
    }
}
