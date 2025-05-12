using Entity.Extension;
using Entity.Models;
using Entity.Result;
using FluentValidation;
using Repository.Contracts;
using Service.Contracts;
using Service.DTOs;
using Service.Validations;

namespace Service
{
    public class CategoryManager(IRepositoryManager manager) : ICategoryService
    {
        private readonly IValidator<CategoryDTO> _categoryDTOValidation = new CategoryDTOValidation();
        public Result<IEnumerable<Category>> GetAllCategories()
        {
            IEnumerable<Category> categories = manager.Category.GetAllCategories();
            if(categories==null) throw new ApiException("Category not found in the database.", "CATEGORY_NOT_FOUND", 404);
            return Result<IEnumerable<Category>>.SuccessResult(categories, "Categories fetched successfully.");
        }

        public Result<Category> GetOneCategory(string id)
        {
            var category = manager.Category.GetOneCategory(id);
            if (category == null) throw new ApiException($"Category with ID '{id}' not found.", "CATEGORY_NOT_FOUND", 404);
            return Result<Category>.SuccessResult(category, $"Category with ID '{id}' retrieved successfully.");
        }
        public Result<Category> CreateOneCategory(CategoryDTO categoryDto)
        {
            var validationResult = _categoryDTOValidation.ValidateAsync(categoryDto);
            if (!validationResult.Result.IsValid)
            {
                var error = validationResult.Result.Errors.First();
                throw new ApiException(error.ErrorMessage, error.ErrorCode, (int)error.CustomState);
            }
            var category = new Category()
            {
                Id = Guid.NewGuid().ToString(),
                Name = categoryDto.Name,
                Description = categoryDto.Description,
                Isactive = categoryDto.Isactive
            };
            manager.Category.CreateOneCategory(category);
            manager.Save();
            return Result<Category>.SuccessResult(category, $"Category '{category.Id}' created successfully.");
        }
        public Result<Category> UpdateOneCategory(string id, CategoryDTO categoryDto)
        {
            var validationResult = _categoryDTOValidation.ValidateAsync(categoryDto);
            if (!validationResult.Result.IsValid)
            {
                var error = validationResult.Result.Errors.First();
                throw new ApiException(error.ErrorMessage, error.ErrorCode, (int)error.CustomState);
            }
            var entity = manager.Category.GetOneCategory(id);
            entity.Name = categoryDto.Name;
            entity.Description = categoryDto.Description;
            entity.Isactive = categoryDto.Isactive;
            manager.Category.Update(entity);
            manager.Save();
            return Result<Category>.SuccessResult(entity,$"Category '{entity.Id}' update successfully.");
        }
        public Result<Category> DeleteOneCategory(string id)
        {
            var entity = manager.Category.GetOneCategory(id);
            if (entity == null) throw new ApiException($"Category with ID '{id}' not found.", "CATEGORY_NOT_FOUND", 404);
            //var product = manager.Product.GetOneProduct()
            //if (product! == null)
            //    throw new ApiException("There are products that have not been deleted", "BEEN_DELETED_PRODUCTS");
            manager.Category.DeleteOneCategory(entity);
            manager.Save();
            return Result<Category>.SuccessResult(entity, $"Category '{entity.Id}' delete successfully.");
        }
    }
}
