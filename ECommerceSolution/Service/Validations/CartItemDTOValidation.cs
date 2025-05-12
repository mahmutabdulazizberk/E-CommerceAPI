using FluentValidation;
using Service.DTOs;

public class CartItemDTOValidation : AbstractValidator<CartItemDTO>
{
    public CartItemDTOValidation()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty().WithErrorCode("PRODUCT_ID_REQUIRED")
            .WithMessage("ProductId boş olamaz.");

        RuleFor(x => x.Quantity)
            .GreaterThan(0).WithErrorCode("QUANTITY_INVALID")
            .WithMessage("Quantity 1’den büyük olmalıdır.");
    }
}