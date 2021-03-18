using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entites.Models
{
    public class Product
    {
        public int ProductID { get; set; }

        [Required]
        [StringLength(40)]
        public string ProductName { get; set; }

        [Column("UnitPrice", TypeName = "money")]
        public decimal? Coast { get; set; }

        [Column("UnitsInStock", TypeName = "smallint")]
        public short Stock { get; set; }

        public bool Discontinued { get; set; }

        // Nav
        [ForeignKey(nameof(Models.Category))]
        public int CategoryID { get; set; }
        public Category Category { get; set; }

    }
}
