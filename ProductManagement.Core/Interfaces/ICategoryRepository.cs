using ProductManagement.Core.Entities;

namespace ProductManagement.Core.Interfaces
{
    public interface ICategoryRepository: IRepository<Category>
    {
        Task<IEnumerable<Category>> GetCategoriesByNameAsync(string name);
    }
}
