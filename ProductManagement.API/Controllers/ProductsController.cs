using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.Application.DTOs;
using ProductManagement.Application.Services;
using ProductManagement.Core.Entities;
using ProductManagement.Core.Interfaces;
using IProductService = ProductManagement.Application.Services.IProductService;

namespace ProductManagement.API.Controllers
{
    public class ProductsController: Controller
    {
        private readonly IProductService _service;

        public ProductsController(ProductService service)
        {
            _service = service;
        }

        [HttpGet("GetAllProducts")]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _service.GetAllProductsAsync();
            return Ok(products);
        }

        [HttpGet("GetProductById{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var products = await _service.GetProductByIdAsync(id);
            return Ok(products);
        }

        [HttpGet("GetProductsByName")]
        public async Task<IActionResult> GetProductsByName(string name)
        {
            var products = await _service.GetProductsByNameAsync(name);
            return Ok(products);
        }
        
        [Authorize(Roles = "Trader")]
        [HttpPost("AddNewProduct")]
        public async Task<IActionResult> AddProduct(ProductDto productDto)
        {
            var product = await _service.AddProductAsync(productDto);

            if (product == null)
                return BadRequest("This Product is already exists!");

            return Ok(product);
        }

        [Authorize(Roles = "Trader")]
        [HttpPut("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct(UpdateProductDto productDto)
        {
            var updatedProduct = await _service.UpdateProductAsync(productDto);

            if (updatedProduct == null)
                return BadRequest("Can't update product");

            return Ok("Updated");
        }

        [Authorize(Roles = "Trader")]
        [HttpDelete("DeleteProduct{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var result = await _service.DeleteProductAsync(id);

            if (!result)
                return BadRequest("This Product isn't exists!");

            return Ok();
        }

    }
}
