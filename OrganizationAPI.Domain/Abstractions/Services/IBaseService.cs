namespace OrganizationAPI.Domain.Abstractions.Services
{
    public interface IBaseService<T, TKey> where T : class
    {
        Task<List<T>> GetAll();
        Task<T> GetById(TKey id);
        Task Add(List<T> entity);
        Task Update(T entity);
        Task Delete(TKey id);
    }
}
