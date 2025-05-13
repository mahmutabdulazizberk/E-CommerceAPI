using FluentValidation;
using Service.DTOs;

namespace Service.Validations;

public class CartItemQuantityDTOValidation : AbstractValidator<CartItemQuantityDTO>
{
    public CartItemQuantityDTOValidation()
    {
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