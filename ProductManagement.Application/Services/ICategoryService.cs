using ProductManagement.Application.DTOs;
using ProductManagement.Application.Helpers;
using ProductManagement.Core.Entities;

namespace ProductManagement.Application.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
        Task<GetCategoryByIdResult> GetCategoryByIdAsync(int id);
        Task<IEnumerable<Category>> GetCategoriesByNameAsync(string name);
        Task<Category> AddCategoryAsync(CategoryDto categoryDto);
        Task<bool> UpdateCategoryAsync(Category category);
        Task<bool> DeleteCategoryAsync(int id);
    }
}
