using TurboKart.Domain.Entities;

namespace TurboKart.Application.Interfaces
{
    public interface ICustomerUseCase
    {
        Task<IEnumerable<Customer>> GetAllCustomers();
        Task<Customer> GetSingleCustomer(object id);
        void Update(Customer customer);
        void Delete(Customer customer);
    }
}
