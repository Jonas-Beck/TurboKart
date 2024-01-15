using TurboKart.Application.Interfaces;
using TurboKart.Domain.Entities;
using TurboKart.Infrastructure.Persistence.Interfaces;

namespace TurboKart.Application.UseCases
{
    public class CustomerUseCase : ICustomerUseCase
    {
        private readonly IUnitOfWork unitOfWork;

        public CustomerUseCase(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }


        public async Task<IEnumerable<Customer>> GetAllCustomers()
        {
            ICustomerRepository customerRepository = unitOfWork.CustomerRepository;
            return await customerRepository.GetAll();
        }

        public async Task<Customer> GetSingleCustomer(object id)
        {
            ICustomerRepository customerRepository = unitOfWork.CustomerRepository;
            return await customerRepository.GetBy(id);
        }

        public async Task Update(Customer customer)
        {
            ICustomerRepository customerRepository = unitOfWork.CustomerRepository;
            customerRepository.Update(customer);

            await unitOfWork.Commit();
        }
        public async Task Delete(Customer customer)
        {
            ICustomerRepository customerRepository = unitOfWork.CustomerRepository;
            customerRepository.Delete(customer);

            await unitOfWork.Commit();
        }

        public static void NewCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }
    }
}
