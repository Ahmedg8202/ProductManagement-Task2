using Microsoft.EntityFrameworkCore;
using ProductManagement.Core.Entities;
using ProductManagement.Core.Interfaces;
using ProductManagement.Infrastructure.Data;

namespace ProductManagement.Infrastructure.Repositories
{
    public class ProductRepository: Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context): base(context)
        {
            _context = context;
        }

        public async Task<Product> GetByProductIdAsync(int productId)
        {
            return await _context.Products
                .FirstOrDefaultAsync(c => c.Id == productId);
        }

        public async Task<IEnumerable<Product>> GetProductByNameAsync(string name)
        {
            return await _context.Products.Where(p => EF.Functions.Like(p.Name, $"%{name}%")).ToListAsync();
        }

    }
}