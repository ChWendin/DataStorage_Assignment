using Data.Entities;

namespace Bussines.Interfaces
{
    public interface ICustomerService
    {
        CustomerEntity CreateCustomer(CustomerEntity customerEntity);
        bool DeleteCustomerById(int id);
        IEnumerable<CustomerEntity> GetAll();
        CustomerEntity GetCustomerById(int id);
        CustomerEntity UpdateCustomer(CustomerEntity customerEntity);
    }
}