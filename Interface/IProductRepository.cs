using project.Entities;

namespace project.Interface
{
    public interface IProductRepository
    {

        Task<Product> GetAsync(int id);
        Task<IReadOnlyList<Product>> GetAllAsync();
        Task<ProductBrand> GetBrandAsync(int id);
        Task<IReadOnlyList<ProductBrand>> GetAllBrandAsync();
        Task<ProductType> GetTypeAsync(int id);
        Task<IReadOnlyList<ProductType>> GetAllTypeAsync();

    }
}
