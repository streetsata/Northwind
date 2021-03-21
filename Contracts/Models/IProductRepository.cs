using Entites.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts.Models
{
    public interface IProductRepository : IRepositoryBase<Product>
    {
        IEnumerable<Product> ProductsByCategory(int id);
        Task<IEnumerable<Product>> ProductsByCategoryAsync(int id);
    }
}
