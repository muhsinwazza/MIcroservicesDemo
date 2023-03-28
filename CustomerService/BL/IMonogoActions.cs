using CustomerService.Models;

namespace CustomerService.BL
{
    public interface IMongoActions
    {
        Task<List<Customer>> GetCustomers();
        Task<Customer> GetCustomerById(string id);
        Task InsertCustomer(Customer customer);
        Task<bool> UpdateCustomer(Customer customer);
        Task<bool> DeleteCustomer(string id);

    }
}
