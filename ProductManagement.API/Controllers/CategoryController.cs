using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.Application.DTOs;
using ProductManagement.Application.Services;
using ProductManagement.Core.Entities;
using ProductManagement.Core.Interfaces;
using ICategoryService = ProductManagement.Application.Services.ICategoryService;

namespace ProductManagement.API.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _service;
        public CategoryController(ICategoryService service)
        {
            _service = service;
        }

        [HttpGet("GetAllCategories")]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _service.GetAllCategoriesAsync();
            return Ok(categories);
        }

        [HttpGet("GetCategoryById{id}")]
        public async Task<ActionResult> GetCategoryById(int id)
        {
            var category = await _service.GetCategoryByIdAsync(id);
            
            if (!category.CategoryExists)
                return BadRequest("There is no Category with this id");

            return Ok(category.Category);
        }

        [HttpGet("GetCategoryByName")]
        public async Task<ActionResult> GetCategoryByName(string name)
        {
            var category = await _service.GetCategoriesByNameAsync(name);

            return Ok(category);
        }

        [Authorize(Roles = "Trader")]
        [HttpPost("AddCategory")]
        public async Task<IActionResult> AddCategory([FromBody]CategoryDto categoryDto)
        {
            var category = await _service.AddCategoryAsync(categoryDto);

            if (category == null)
                return BadRequest("This Category is already exists!");

            return Ok(category);
        }

        [Authorize(Roles = "Trader")]
        [HttpPut("UpdateCategory")]
        public async Task<IActionResult> UpdateCategory(Category category)
        {
            var updatedCategory = await _service.UpdateCategoryAsync(category);

            if (updatedCategory == null)
                return BadRequest("Can't update category");

            return Ok("Updated");
        }

        [Authorize(Roles = "Trader")]
        [HttpDelete("DeleteCategory{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var result = await _service.DeleteCategoryAsync(id);
            
            if (!result)
                return BadRequest("This Category isn't exists!");

            return Ok();
        }
    }
}
