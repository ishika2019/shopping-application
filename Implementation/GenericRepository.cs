using Microsoft.EntityFrameworkCore;
using project.Data;
using project.Entities;
using project.Interface;
using project.Specification;

namespace project.Implementation
{
    public class GenericRepository<T> : IGenericRepository<T> where T:BaseEntity
    {
        private readonly StoreDbContext context;

        public GenericRepository(StoreDbContext context)
        {
            this.context = context;
        }

        public async Task<int> CountAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }

        public async Task<T> getByIdAsync(int id)
        {
            return await context.Set<T>().FindAsync(id);
        }

        public async Task<T> getEntityWithSpecification(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<T>> listAllAsync()
        {
            return await context.Set<T>().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.getQuery(context.Set<T>().AsQueryable(),spec);
        }
    }

}
