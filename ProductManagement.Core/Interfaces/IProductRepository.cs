using ProductManagement.Core.Entities;

namespace ProductManagement.Core.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> GetProductByNameAsync(string name);
        Task<Product> GetByProductIdAsync(int productId);
    }
}
