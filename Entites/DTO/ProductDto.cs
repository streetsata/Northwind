using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entites.DTO
{
    public class ProductDto
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal? Coast { get; set; }
        public short Stock { get; set; }
        public bool Discontinued { get; set; }

        public string Product { get; set; }
    }
}
