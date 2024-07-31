using Microsoft.EntityFrameworkCore;
using project.Data;
using project.Entities;
using project.Interface;

namespace project.Implementation
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreDbContext context;

        public ProductRepository(StoreDbContext context)
        {
            this.context = context;
        }
        public  async Task<Product> GetAsync(int id)
        {
            var result = await context.Products.Include(p => p.productbrand).Include(p => p.producttype).FirstOrDefaultAsync(p=>p.Id==id);
            return result;
        }

        public async Task<IReadOnlyList<Product>> GetAllAsync()
        {
            var result=await context.Products.Include(p => p.productbrand).Include(p => p.producttype).ToListAsync();
            return result;
        }

        public async Task<ProductBrand> GetBrandAsync(int id)
        {
            var result = await context.Productbrand.FindAsync(id);
            return result;
        }

        public async Task<IReadOnlyList<ProductBrand>> GetAllBrandAsync()
        {
            var result = await context.Productbrand.ToListAsync();
            return result;
        }

        public async Task<ProductType> GetTypeAsync(int id)
        {
            var result = await context.Producttype.FindAsync(id);
            return result;
        }

        public async Task<IReadOnlyList<ProductType>> GetAllTypeAsync()
        {
            var result = await context.Producttype.ToListAsync();
            return result;
        }
    }
}
