using Entites.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.Models
{
    public interface ICategoryRepository : IRepositoryBase<Category>
    {
        IEnumerable<Category> GetAllCategories();
        Category GetCategoryById(int categoryId);
        Category GetCategoryWithDetails(int categoryId);
        void CreateCategory(Category category);
        void UpdateCategory(Category category);
    }
}
