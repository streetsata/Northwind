using Entites.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Models
{
    public interface ICategoryRepository : IRepositoryBase<Category>
    {
        IEnumerable<Category> GetAllCategories();
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
        Category GetCategoryById(int categoryId);
        Task<Category> GetCategoryByIdAsync(int categoryId);
        Category GetCategoryWithDetails(int categoryId);
        Task<Category> GetCategoryWithDetailsAsync(int categoryId);
        void CreateCategory(Category category);
        void UpdateCategory(Category category);
        void DeleteCategory(Category category);
    }
}
