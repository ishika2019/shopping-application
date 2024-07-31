using project.Entities;
using project.Specification;

namespace project.Interface
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T> getByIdAsync(int id);
        Task<IReadOnlyList<T>> listAllAsync();

        Task<T> getEntityWithSpecification(ISpecification<T> spec);
        Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);

        Task<int> CountAsync(ISpecification<T> spec); 
    }
}
