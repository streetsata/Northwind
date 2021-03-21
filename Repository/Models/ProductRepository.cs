using Contracts.Models;
using Entites;
using Entites.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(NorthwindContext northwindContext)
            : base(northwindContext)
        {
        }

        public IEnumerable<Product> ProductsByCategory(int id)
        {
            return FindByCondition(p => p.CategoryID.Equals(id)).ToList();
        }

        public async Task<IEnumerable<Product>> ProductsByCategoryAsync(int id)
        {
            return await FindByCondition(p => p.CategoryID.Equals(id)).ToListAsync();
        }
    }
}
