using Entity.Models;
using FluentValidation;
using Service.DTOs;

namespace Service.Validations;

public class CategoryDTOValidation : AbstractValidator<CategoryDTO>
{
    public CategoryDTOValidation()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Category name is required.")
            .WithErrorCode("CATEGORY_NAME_REQUIRED")
            .WithState(_ => 400)
            .MaximumLength(100)
            .WithMessage("Category name must not exceed 100 characters.")
            .WithErrorCode("CATEGORY_NAME_TOO_LONG")
            .WithState(_ => 400);

        RuleFor(x => x.Description)
            .MaximumLength(500)
            .WithMessage("Category description must not exceed 500 characters.")
            .When(x => !string.IsNullOrEmpty(x.Description))
            .WithErrorCode("CATEGORY_DESCRIPTION_TOO_LONG")
            .WithState(_ => 400);
    }
}