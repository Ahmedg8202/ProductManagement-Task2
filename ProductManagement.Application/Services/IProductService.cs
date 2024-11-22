using ProductManagement.Application.DTOs;
using ProductManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Application.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(int id);
        Task<IEnumerable<Product>> GetProductsByNameAsync(string name);
        Task<Product> AddProductAsync(ProductDto productDto);
        Task<bool> UpdateProductAsync(UpdateProductDto productDto);
        Task<bool> DeleteProductAsync(int id);
    }
}
