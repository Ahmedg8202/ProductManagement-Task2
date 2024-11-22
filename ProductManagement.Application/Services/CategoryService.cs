using AutoMapper;
using ProductManagement.Application.Helpers;
using ProductManagement.Application.DTOs;
using ProductManagement.Core.Entities;
using ProductManagement.Core.Interfaces;

namespace ProductManagement.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _unitOfWork.CategoryRepository.GetAllAsync();
        }

        public async Task<GetCategoryByIdResult> GetCategoryByIdAsync(int id)
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(id);

            if (category == null) return new GetCategoryByIdResult { CategoryExists = false };

            return new GetCategoryByIdResult {
                CategoryExists = true,
                Category = category
            };
        }

        public async Task<IEnumerable<Category>> GetCategoriesByNameAsync(string name)
        {
            return await _unitOfWork.CategoryRepository.GetCategoriesByNameAsync(name);
        }

        public async Task<Category> AddCategoryAsync(CategoryDto categoryDto)
        {
            Category category = _mapper.Map<Category>(categoryDto);

            var categoryExists = await GetCategoriesByNameAsync(categoryDto.Name);
            if (categoryExists == null) return null;

            Category addedCategory = await _unitOfWork.CategoryRepository.AddAsync(category);
            
            await _unitOfWork.CompleteAsync();

            return addedCategory;
        }

        public async Task<bool> UpdateCategoryAsync(Category category)
        {
            var updatedCategory = await _unitOfWork.CategoryRepository.UpdateAsync(category);

            if (!updatedCategory) return false;

            return true;
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            return await _unitOfWork.CategoryRepository.DeleteAsync(id);
        }
    }
}
