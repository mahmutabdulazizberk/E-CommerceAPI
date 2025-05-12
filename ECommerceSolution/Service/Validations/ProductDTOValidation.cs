using FluentValidation;
using Service.DTOs;

namespace Service.Validations;

public class ProductDTOValidation : AbstractValidator<ProductDTO>
{
    public ProductDTOValidation()
    {
        RuleFor(p => p.Name)
            .NotEmpty()
            .WithMessage("Product name is required.")
            .WithErrorCode("PRODUCT_NAME_REQUIRED")
            .WithState(_ => 400)
            .MaximumLength(255)
            .WithMessage("Product name must not exceed 255 characters.")
            .WithErrorCode("PRODUCT_NAME_TOO_LONG")
            .WithState(_ => 400);

        RuleFor(p => p.Description)
            .MaximumLength(4000)
            .WithMessage("Product description must not exceed 4000 characters.")
            .WithErrorCode("PRODUCT_DESCRIPTION_TOO_LONG")
            .When(p => !string.IsNullOrEmpty(p.Description))
            .WithState(_ => 400);

        RuleFor(p => p.Price)
            .GreaterThan(0)
            .WithMessage("Price must be greater than zero.")
            .WithErrorCode("INVALID_PRICE")
            .WithState(_ => 400);

        RuleFor(p => p.Stockquantity)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Stock quantity must be a non-negative value.")
            .WithErrorCode("INVALID_STOCK_QUANTITY")
            .WithState(_ => 400);

        RuleFor(p => p.Categoryid)
            .NotEmpty()
            .WithMessage("Category ID is required.")
            .WithErrorCode("CATEGORY_ID_REQUIRED")
            .WithState(_ => 400)
            .MaximumLength(50)
            .WithMessage("Category ID must not exceed 50 characters.")
            .WithErrorCode("CATEGORY_ID_TOO_LONG")
            .WithState(_ => 400);
    }
}