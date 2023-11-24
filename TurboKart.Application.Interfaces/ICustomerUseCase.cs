using TurboKart.Domain.Entities;

namespace TurboKart.Application.Interfaces
{
    public interface ICustomerUseCase
    {
        IEnumerable<Customer> GetAllCustomers();
        Customer GetSingleCustomer(object id);
        void Update(Customer customer);
    }
}
