using OrganizationAPI.Domain.Abstractions.Repositories;
using OrganizationAPI.Domain.Abstractions.Services;

namespace OrganizationAPI.Domain.Services
{
    public class BaseService<T, TKey> : IBaseService<T, TKey> where T : class
    {
        private readonly IRepository<T, TKey> repository;
        public BaseService(IRepository<T, TKey> repository)
        {
            this.repository = repository;
        }

        public virtual async Task<List<T>> GetAll()
        {
            return await repository.GetAll();
        }

        public virtual async Task<T> GetById(TKey id)
        {
            return await repository.GetById(id);
        }

        public virtual async Task Add(List<T> entity)
        {
            await repository.Add(entity);
        }

        public virtual async Task Update(T entity)
        {
            await repository.Update(entity);
        }

        public virtual async Task Delete(TKey id)
        {
            await repository.Delete(id);
        }
    }
}
