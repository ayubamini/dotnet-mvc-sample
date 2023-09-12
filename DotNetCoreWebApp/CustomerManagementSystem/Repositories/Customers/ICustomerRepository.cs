using CustomerManagementSystem.DATA.Entities;

namespace CustomerManagementSystem.Repositories.Customers
{
    public interface ICustomerRepository : IRepositoryBaseAsync<Customer>
    {
        IQueryable<Customer> GetAll();

        Task<List<Customer>> GetAll(int pageSize, int pageNumber);

        Task<int> GetNumberOfCustomers();

        Task RemoveAsync(Customer customer);
    }
}
