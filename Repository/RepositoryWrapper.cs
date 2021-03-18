using Contracts;
using Contracts.Models;
using Entites;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private NorthwindContext _northwindContext;
        private ICategoryRepository _category;
        private IProductRepository _product;

        public ICategoryRepository Category
        {
            get 
            {
                if (_category == null)
                {
                    _category = new CategoryRepository(_northwindContext);
                }

                return _category;
            }
        }

        public IProductRepository Product
        {
            get
            {
                if (_product == null)
                {
                    _product = new ProductRepository(_northwindContext);
                }

                return _product;
            }
        }

        public RepositoryWrapper(NorthwindContext northwindContext)
        {
            _northwindContext = northwindContext;
        }

        public void Save()
        {
            _northwindContext.SaveChanges();
        }
    }
}
