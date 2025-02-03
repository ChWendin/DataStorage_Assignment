using Data.Entities;

namespace Bussines.Interfaces
{
    public interface IProductService
    {
        ProductEntity CreateProduct(ProductEntity productEntity);
        bool DeleteProductById(int id);
        IEnumerable<ProductEntity> GetAllProducts();
        ProductEntity GetProductById(int id);
        ProductEntity UpdateProduct(ProductEntity productEntity);
    }
}