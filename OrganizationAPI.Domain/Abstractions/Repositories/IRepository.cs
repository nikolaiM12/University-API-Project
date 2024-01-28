namespace OrganizationAPI.Domain.Abstractions.Repositories
{
    public interface IRepository<T, TKey> 
    {
        Task<List<T>> GetAll();
        Task<T> GetById(TKey id);
        Task Add(List<T> entity);
        Task Update(T entity);
        Task Delete(TKey id);
    }
}
