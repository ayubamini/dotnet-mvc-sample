using CustomerManagementSystem.DATA.Entities;

namespace CustomerManagementSystem.Repositories.Customers
{
    public interface ICustomerRepository : IRepositoryBaseAsync<Customer>
    {
        new Task<List<Customer>> GetAllAsync();

        Task RemoveAsync(Customer customer);
    }
}
