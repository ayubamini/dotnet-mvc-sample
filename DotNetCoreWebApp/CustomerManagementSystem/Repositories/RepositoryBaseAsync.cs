using CustomerManagementSystem.DATA.Context;

namespace CustomerManagementSystem.Repositories
{
    public class RepositoryBaseAsync<T> : IRepositoryBaseAsync<T> where T : class
    {
        private readonly ApplicationDbContext context;

        public RepositoryBaseAsync(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<T> AddAsync(T entity)
        {
            await context.AddAsync(entity);

            await context.SaveChangesAsync();

            return entity;
        }

        public async Task AddRangeAsync(List<T> entities)
        {
            await context.AddRangeAsync(entities);

            await context.SaveChangesAsync();
        }

        public async Task RemoveAsync(int id)
        {
            var entity = await context.Set<T>().FindAsync(id);

            context.Set<T>().Remove(entity!);

            await context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            var entity = await context.Set<T>().FindAsync(id);

            if (entity == null)
                return false;

            return true;
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await context.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int? id)
        {
            if (id == null)
            {
                return null;
            }
            return await context.Set<T>().FindAsync(id);
        }

        public async Task UpdateAsync(T entity)
        {
            context.Entry(entity).State = EntityState.Modified;
           
            await context.SaveChangesAsync();
        }
    }
}
