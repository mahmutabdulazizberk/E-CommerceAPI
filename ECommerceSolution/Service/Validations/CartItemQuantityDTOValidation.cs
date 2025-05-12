using FluentValidation;
using Service.DTOs;

namespace Service.Validations;
class CartItemQuantityDTOValidation : AbstractValidator<CartItemQuantityDTO>
{
    public CartItemQuantityDTOValidation()
    {
        RuleFor(x => x.Quantity)
            .GreaterThan(0)
            .WithMessage("Quantity must be at least 1")
            .WithErrorCode("INVALID_QUANTITY")
            .WithState(_ => 400);
    }
}
