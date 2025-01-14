using InsuranceAPI.Data;
using InsuranceAPI.Entities;
using InsuranceAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InsuranceAPI.Repository
{
    public class CustomerRepository(InsuranceDbContext context) : ICustomer
    {
        public async Task<Customer> CreateAsync(Customer entity)
        {
            var newCustomer = context.Customers.Add(entity).Entity;
            await context.SaveChangesAsync();
            return newCustomer;
        }

        public async Task<string> DeleteAsync(int id)
        {
            var customer = await context.Customers.FindAsync(id);
            if(customer is null)
            {
                throw new Exception("Customer not found");
            }
            context.Customers.Remove(customer);
            await context.SaveChangesAsync();
            return "Customer successfully removed";
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            var customers = await context.Customers.ToListAsync();
            return customers;
        }

        public async Task<Customer> GetByIdAsync(int id)
        {
            var customer = await context.Customers.FindAsync(id);
            if (customer is null)
            {
                throw new Exception("Customer not found");
            }
            return customer;
        }

        public async Task<Customer> UpdateAsync(int id, Customer entity)
        {
            var customer = await context.Customers.FindAsync(id);
            if (customer is null)
            {
                throw new Exception("Customer not found");
            }
            customer!.FirstName = entity.FirstName;
            customer!.LastName = entity.LastName;
            customer!.DateOfBirth = entity.DateOfBirth;
            customer!.Email = entity.Email;
            customer!.Phone = entity.Phone;
            customer!.Address = entity.Address;
            customer!.KycStatus = entity.KycStatus;

            await context.SaveChangesAsync();
            return customer;

        }
    }
}
