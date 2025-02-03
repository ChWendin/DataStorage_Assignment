

using Bussines.Interfaces;
using Data.Contexts;
using Data.Entities;

namespace Bussines.Services;

public class ProductService(DataContext context) : IProductService
{
    private readonly DataContext _context = context;


    public ProductEntity CreateProduct(ProductEntity productEntity) //Skapa DTO/Modell att stoppa in här istället
    {
        _context.Products.Add(productEntity);
        _context.SaveChanges();

        return productEntity;
    }

    public IEnumerable<ProductEntity> GetAllProducts()
    {
        return _context.Products;
    }

    public ProductEntity GetProductById(int id)
    {
        var productEntity = _context.Products.SingleOrDefault(x => x.Id == id);
        return productEntity ?? null!;
    }

    public ProductEntity UpdateProduct(ProductEntity productEntity)
    {
        _context.Products.Update(productEntity);
        _context.SaveChanges();

        return productEntity;
    }

    public bool DeleteProductById(int id)
    {
        var productEntity = _context.Products.SingleOrDefault(x => x.Id == id);
        if (productEntity != null)
        {
            _context.Remove(productEntity);
            _context.SaveChanges();

            return true;
        }
        else
        {
            return false;
        }
    }
}
