using FluentValidation;
using Service.DTOs;

public class CartItemDTOValidation : AbstractValidator<CartItemDTO>
{
    public CartItemDTOValidation()
    {
        RuleFor(x => x.Productid)
            .NotEmpty()
            .WithMessage("Product ID is required.")
            .WithErrorCode("PRODUCT_ID_REQUIRED")
            .WithState(_ => 400)
            .MaximumLength(50)
            .WithMessage("Product ID must not exceed 50 characters.")
            .WithErrorCode("PRODUCT_ID_TOO_LONG")
            .WithState(_ => 400);

        RuleFor(x => x.Quantity)
            .GreaterThan(0)
            .WithMessage("Quantity must be greater than zero.")
            .WithErrorCode("QUANTITY_MUST_BE_GREATER_THAN_ZERO")
            .WithState(_ => 400)
            .LessThanOrEqualTo(1000)
            .WithMessage("Quantity must not exceed 1000.")
            .WithErrorCode("QUANTITY_TOO_LARGE")
            .WithState(_ => 400);
    }
}