using FluentValidation;
using Service.DTOs;
using System;
using Entity.Models;

public class AuthenticationValidation : AbstractValidator<AuthenticationDTO>
{
    public AuthenticationValidation()
    {
        RuleFor(x => x.Key)
            .NotEmpty()
            .WithMessage("Key is required.")
            .WithErrorCode("TOKEN_KEY_REQUIRED")
            .WithState(_ => 400) // Örnek HTTP durum kodu, isteğe bağlı
            .MinimumLength(32) // Örnek minimum uzunluk, güvenlik için önemli olabilir
            .WithMessage("Key must be at least 32 characters long.")
            .WithErrorCode("TOKEN_KEY_TOO_SHORT")
            .WithState(_ => 400)
            .MaximumLength(256) // Örnek maksimum uzunluk
            .WithMessage("Key must not exceed 256 characters.")
            .WithErrorCode("TOKEN_KEY_TOO_LONG")
            .WithState(_ => 400);

        // Issuer için doğrulama kuralları
        RuleFor(x => x.Issuer)
            .NotEmpty()
            .WithMessage("Issuer is required.")
            .WithErrorCode("TOKEN_ISSUER_REQUIRED")
            .WithState(_ => 400)
            .MaximumLength(100) // Örnek maksimum uzunluk
            .WithMessage("Issuer must not exceed 100 characters.")
            .WithErrorCode("TOKEN_ISSUER_TOO_LONG")
            .WithState(_ => 400);

        // Audience için doğrulama kuralları
        RuleFor(x => x.Audience)
            .NotEmpty()
            .WithMessage("Audience is required.")
            .WithErrorCode("TOKEN_AUDIENCE_REQUIRED")
            .WithState(_ => 400)
            .MaximumLength(100) // Örnek maksimum uzunluk
            .WithMessage("Audience must not exceed 100 characters.")
            .WithErrorCode("TOKEN_AUDIENCE_TOO_LONG")
            .WithState(_ => 400);
    }
}