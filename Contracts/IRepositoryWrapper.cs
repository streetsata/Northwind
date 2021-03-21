using Contracts.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IRepositoryWrapper
    {
        ICategoryRepository Category { get; }
        IProductRepository Product { get; }
        void Save();
        Task SaveAsync();
    }
}
