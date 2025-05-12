using Entity.Models;

namespace Repository.Contracts;

public interface IProductRepository : IRepositoryBase<Product>
{
    IQueryable<Product> GetAllProducts();
    IQueryable<Product> GetProductsByCategory(string categoryId);
    Product GetOneProduct(string id);
    void CreateOneProduct(Product product);
    void UpdateOneProduct(Product product);
    void DeleteOneProduct(Product product);
}