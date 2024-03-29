﻿using TurboKart.Domain.Entities;

namespace TurboKart.Application.Interfaces
{
    public interface ICustomerUseCase
    {
        Task<IEnumerable<Customer>> GetAllCustomers();
        Task<Customer> GetSingleCustomer(object id);
        Task Update(Customer customer);
        Task Delete(Customer customer);
    }
}
