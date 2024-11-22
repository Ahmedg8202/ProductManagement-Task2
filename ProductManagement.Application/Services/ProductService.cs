using AutoMapper;
using ProductManagement.Application.DTOs;
using ProductManagement.Core.Entities;
using ProductManagement.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Application.Services
{
    public class ProductService: IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _unitOfWork.ProductRepository.GetAllAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _unitOfWork.ProductRepository.GetByIdAsync(id);
        }
        public async Task<IEnumerable<Product>> GetProductsByNameAsync(string name)
        {
            return await _unitOfWork.ProductRepository.GetProductByNameAsync(name);
        }

        public async Task<Product> AddProductAsync(ProductDto productDto)
        {
            Product product = _mapper.Map<Product>(productDto);

            var productExists = await GetProductsByNameAsync(productDto.Name);
            if (productExists.Any()) return null;

            Product addedProduct = await _unitOfWork.ProductRepository.AddAsync(product);

            return addedProduct;
        }

        public async Task<bool> UpdateProductAsync(UpdateProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            var updatedProduct = await _unitOfWork.ProductRepository.UpdateAsync(product);

            if (!updatedProduct) return false;

            return true;
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            return await _unitOfWork.ProductRepository.DeleteAsync(id);;
        }

    }
}
