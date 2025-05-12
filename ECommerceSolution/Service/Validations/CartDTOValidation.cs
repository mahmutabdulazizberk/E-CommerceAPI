using FluentValidation;
using Service.DTOs;
using System;

public class CartDTOValidation : AbstractValidator<CartDTO>
{
    public CartDTOValidation()
    {
        RuleFor(x => x.Userid)
            .NotEmpty()
            .WithMessage("User ID is required.")
            .WithErrorCode("USER_ID_REQUIRED")
            .WithState(_ => 400)
            .MaximumLength(50)
            .WithMessage("User ID must not exceed 50 characters.")
            .WithErrorCode("USER_ID_TOO_LONG")
            .WithState(_ => 400);
    }
}