using Contracts.Models;
using Entites;
using Entites.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Models
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(NorthwindContext northwindContext)
            : base(northwindContext)
        {
        }
    }
}
