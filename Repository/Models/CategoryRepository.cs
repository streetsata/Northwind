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
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        #region Sync Methods
        public CategoryRepository(NorthwindContext northwindContext)
            : base(northwindContext)
        {
        }

        public void CreateCategory(Category category)
        {
            Create(category);
        }

        public void DeleteCategory(Category category)
        {
            Delete(category);
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
        #endregion

        #region Async Methods
        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await FindAll()
                 .OrderBy(c => c.CategoryName)
                 .ToListAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(int categoryId)
        {
            return await FindByCondition(category => category.CategoryID.Equals(categoryId))
                  .FirstOrDefaultAsync();
        }

        public async Task<Category> GetCategoryWithDetailsAsync(int categoryId)
        {
            return await FindByCondition(category => category.CategoryID.Equals(categoryId))
               .Include(p => p.Products)
               .FirstOrDefaultAsync();
        }
        #endregion
    }
}
