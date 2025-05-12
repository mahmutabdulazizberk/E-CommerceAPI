using Entity.Models;

namespace Repository.Contracts;

public interface ICategoryRepository : IRepositoryBase<Category>
{
    IQueryable<Category> GetAllCategories();
    Category GetOneCategory(string id);
    void CreateOneCategory(Category category);
    void UpdateOneCategory(Category category);
    void DeleteOneCategory(Category category);
}