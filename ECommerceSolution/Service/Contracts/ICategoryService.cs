using Entity.Models;
using Entity.Result;
using Service.DTOs;

namespace Service.Contracts;

public interface ICategoryService
{
    Result<IEnumerable<Category>> GetAllCategories();
    Result <Category> GetOneCategory(string id);
    Result<Category> CreateOneCategory(CategoryDTO categoryDto);
    Result<Category> UpdateOneCategory(string id, CategoryDTO categoryDto);
    Result<Category> DeleteOneCategory(string id);
}