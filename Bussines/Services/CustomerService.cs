
using Bussines.Interfaces;
using Data.Contexts;
using Data.Entities;

namespace Bussines.Services;

public class CustomerService(DataContext context) : ICustomerService
{
    private readonly DataContext _context = context;


    public CustomerEntity CreateCustomer(CustomerEntity customerEntity) //Skapa DTO/Modell att stoppa in här istället
    {
        _context.Customers.Add(customerEntity);
        _context.SaveChanges();

        return customerEntity;
    }

    public IEnumerable<CustomerEntity> GetAll()
    {
        return _context.Customers;
    }

    public CustomerEntity GetCustomerById(int id)
    {
        var customerEntity = _context.Customers.SingleOrDefault(x => x.Id == id);
        return customerEntity ?? null!;
    }

    public CustomerEntity UpdateCustomer(CustomerEntity customerEntity)
    {
        _context.Customers.Update(customerEntity);
        _context.SaveChanges();

        return customerEntity;
    }

    public bool DeleteCustomerById(int id)
    {
        var customerEntity = _context.Customers.SingleOrDefault(x => x.Id == id);
        if (customerEntity != null)
        {
            _context.Remove(customerEntity);
            _context.SaveChanges();

            return true;
        }
        else
        {
            return false;
        }
    }
}
