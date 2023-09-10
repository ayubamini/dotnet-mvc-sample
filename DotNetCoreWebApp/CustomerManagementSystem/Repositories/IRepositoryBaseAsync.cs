namespace CustomerManagementSystem.Repositories
{
    public interface IRepositoryBaseAsync<T> where T : class
    {
        Task<T> GetByIdAsync(int? id);
        Task<List<T>> GetAllAsync();
        Task<T> AddAsync(T entity);
        Task AddRangeAsync(List<T> entities);
        Task UpdateAsync(T entity);
        Task RemoveAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
