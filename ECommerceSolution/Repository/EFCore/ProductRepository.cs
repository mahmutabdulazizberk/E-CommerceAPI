using Entity.Models;
using Repository.Context;
using Repository.Contracts;

namespace Repository.EFCore;

public class ProductRepository(AppDbContext context) : RepositoryBase<Product>(context), IProductRepository
{
    public IQueryable<Product> GetAllProducts() => FindAll().OrderBy(u => u.Id);
    public IQueryable<Product> GetProductsByCategory(string categoryId) => FindAll().Where(p => p.Categoryid == categoryId).OrderBy(p => p.Id);
    public Product GetOneProduct(string id) => FindById(b => b.Id == id).SingleOrDefault()!; //singleordefault ile risk almak istemiyoruz.
    public void CreateOneProduct(Product product) => Create(product);
    public void UpdateOneProduct(Product product) => Update(product);
    public void DeleteOneProduct(Product product) => Delete(product);
}