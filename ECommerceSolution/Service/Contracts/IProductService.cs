using Entity.Models;
using Entity.Result;
using Service.DTOs;

namespace Service.Contracts;

public interface IProductService
{
    Result<IEnumerable<Product>> GetAllProducts();
    Result<IEnumerable<Product>> GetProductsByCategory(string categoryId);
    Result<Product> GetOneProduct(string id);
    Result<Product> CreateOneProduct(ProductDTO productDto);
    Result<Product> UpdateOneProduct(string id, ProductDTO productDto);
    Result<Product> DeleteOneProduct(string id);
}