using CustomerManagementSystem.DATA.Context;
using CustomerManagementSystem.DATA.Entities;

namespace CustomerManagementSystem.Repositories.Customers
{
    public class CustomerRepository : RepositoryBaseAsync<Customer>, ICustomerRepository
    {
        private readonly ApplicationDbContext _context;

        public CustomerRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public new async Task<List<Customer>> GetAllAsync()
        {
            var result = await _context.Set<Customer>().OrderBy(q => q.Id).ToListAsync();

            return result;
        }

        public async Task RemoveAsync(Customer customer)
        {
            var entity = await _context.Set<Customer>().FindAsync(customer.Id);

            if (entity != null)
            {
                _context.Set<Customer>().Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
