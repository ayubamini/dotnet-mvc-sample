using CustomerManagementSystem.DATA.Context;
using CustomerManagementSystem.DATA.Entities;
using Microsoft.EntityFrameworkCore;

namespace CustomerManagementSystem.Repositories.Customers
{
    public class CustomerRepository : RepositoryBaseAsync<Customer>, ICustomerRepository
    {
        private readonly ApplicationDbContext _context;

        public CustomerRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable<Customer> GetAll()
        {
            var result = _context.Set<Customer>().AsQueryable();

            return result;
        }

        public async Task<List<Customer>> GetAll(int pageSize, int pageNumber)
        {
            var result = await _context.Set<Customer>()
               .Skip((pageNumber - 1) * pageSize)
               .Take(pageSize)
               .OrderBy(c => c.Id).ToListAsync();

            return result;
        }

        public async Task<int> GetNumberOfCustomers()
        {
            var result = await _context.Set<Customer>().CountAsync();

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
