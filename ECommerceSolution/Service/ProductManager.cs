using Entity.Extension;
using Entity.Models;
using Entity.Result;
using FluentValidation;
using Repository.Contracts;
using Service.Contracts;
using Service.DTOs;
using Service.Validations;

namespace Service;

public class ProductManager(IRepositoryManager manager) : IProductService
{
    private readonly IValidator<ProductDTO> _productDTOValidation = new ProductDTOValidation();
    public Result<IEnumerable<Product>> GetAllProducts()
    {
        IQueryable<Product> products = manager.Product.GetAllProducts();
        if (products == null) throw new ApiException("Products not found in the database.", "PRODUCTS_NOT_FOUND", 404);
        return Result<IEnumerable<Product>>.SuccessResult(products, "Products fetched successfully.");
    }

    public Result<IEnumerable<Product>> GetProductsByCategory(string categoryId)
    {
        var products = manager.Product.GetProductsByCategory(categoryId);

        if (!products.Any())
            throw new ApiException(
                $"No products found for category '{categoryId}'.",
                "PRODUCTS_NOT_FOUND_BY_CATEGORY",
                404);

        return Result<IEnumerable<Product>>
            .SuccessResult(products, $"Products for category '{categoryId}' fetched successfully.");
    }

    public Result<Product> GetOneProduct(string id)
    {
        var product = manager.Product.GetOneProduct(id);
        if (product is null) throw new ApiException("Product not found the database.", "PRODUCT_NOT_FOUND", 404);
        return Result<Product>.SuccessResult(product, "Product fetched successfully");
    }

    public Result<Product> CreateOneProduct(ProductDTO productDto)
    {
        var validationResult = _productDTOValidation.ValidateAsync(productDto);
        if (!validationResult.Result.IsValid)
        {
            var error = validationResult.Result.Errors.First();
            throw new ApiException(error.ErrorMessage,error.ErrorCode,(int)error.CustomState);
        }
        var product = new Product()
        {
            Id = Guid.NewGuid().ToString(),
            Name = productDto.Name,
            Description = productDto.Description,
            Price = productDto.Price,
            Stockquantity = productDto.Stockquantity,
            Categoryid = productDto.Categoryid,
            Isactive = productDto.Isactive,
        };
        manager.Product.CreateOneProduct(product);
        manager.Save();
        return Result<Product>.SuccessResult(product,$"Product '{product.Id}' created successfully");
    }

    public Result<Product> UpdateOneProduct(string id, ProductDTO productDto)
    {
        var validationResult = _productDTOValidation.ValidateAsync(productDto);
        if (!validationResult.Result.IsValid)
        {
            var error = validationResult.Result.Errors.First();
            throw new ApiException(error.ErrorMessage, error.ErrorCode, (int)error.CustomState);
        }

        var entity = manager.Product.GetOneProduct(id);
        entity.Name= productDto.Name;
        entity.Description= productDto.Description;
        entity.Price= productDto.Price;
        entity.Stockquantity= productDto.Stockquantity;
        entity.Categoryid= productDto.Categoryid;
        entity.Isactive= productDto.Isactive;

        manager.Product.UpdateOneProduct(entity);
        manager.Save();
        return Result<Product>.SuccessResult(entity, $"Product '{entity.Id}' update successfully");
    }

    public Result<Product> DeleteOneProduct(string id)
    {
        var entity=manager.Product.GetOneProduct(id);
        if (entity is null) throw new ApiException($"Product with ID '{id}' not found.","PRODUCT_NOT_FOUND",404);
        manager.Product.DeleteOneProduct(entity);
        manager.Save();
        return Result<Product>.ErrorResult($"Product '{entity.Id}' delete successfully");
    }
}