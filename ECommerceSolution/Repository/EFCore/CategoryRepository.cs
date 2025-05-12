using Entity.Models;
using Repository.Context;
using Repository.Contracts;

namespace Repository.EFCore;

public class CategoryRepository(AppDbContext context) : RepositoryBase<Category>(context), ICategoryRepository
{
    public IQueryable<Category> GetAllCategories() => FindAll().OrderBy(b => b.Id);
    public Category GetOneCategory(string id) => FindById(b=>b.Id == id).SingleOrDefault()!; //singleordefault ile risk almak istemiyoruz.
    public void CreateOneCategory(Category category) => Create(category);
    public void UpdateOneCategory(Category category) => Update(category);
    public void DeleteOneCategory(Category category) => Delete(category);
}