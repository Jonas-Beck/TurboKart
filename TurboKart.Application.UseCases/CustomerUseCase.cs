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
            // Initialize the customer repository from the unit of work
            ICustomerRepository customerRepository = unitOfWork.CustomerRepository;
            
            // Return IEnumerable<Customer> obtained by calling GetALl() on the customer repository
            return await customerRepository.GetAll();
        }

        public async Task<Customer> GetSingleCustomer(object id)
        {
            // Initialize the customer repository from the unit of work
            ICustomerRepository customerRepository = unitOfWork.CustomerRepository;
            
            // Return Customer obtained by calling GetBy(id) on the customer repository
            return await customerRepository.GetBy(id);
        }

        public async Task Update(Customer customer)
        {
            // Initialize the customer repository from the unit of work
            ICustomerRepository customerRepository = unitOfWork.CustomerRepository;
            
            // Update the customer 
            customerRepository.Update(customer);

            // Commit changes to the database 
            await unitOfWork.Commit();
        }
        public async Task Delete(Customer customer)
        {
            // Initialize the customer repository from the unit of work
            ICustomerRepository customerRepository = unitOfWork.CustomerRepository;
            
            // Delete the customer
            customerRepository.Delete(customer);

            // Commit changes to the database
            await unitOfWork.Commit();
        }

        public static void NewCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }
    }
}
