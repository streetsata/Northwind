using Contracts.Models;
using Entites;
using Entites.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository.Models
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(NorthwindContext northwindContext)
            : base(northwindContext)
        {
        }

        public void CreateCategory(Category category)
        {
            Create(category);
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return FindAll()
                 .OrderBy(c => c.CategoryName)
                 .ToList();
        }

        public Category GetCategoryById(int categoryId)
        {
            return FindByCondition(category => category.CategoryID.Equals(categoryId))
                  .FirstOrDefault();
        }

        public Category GetCategoryWithDetails(int categoryId)
        {
            return FindByCondition(category => category.CategoryID.Equals(categoryId))
                .Include(p => p.Products)
                .FirstOrDefault();
        }

        public void UpdateCategory(Category category)
        {
            Update(category);
        }
    }
}
