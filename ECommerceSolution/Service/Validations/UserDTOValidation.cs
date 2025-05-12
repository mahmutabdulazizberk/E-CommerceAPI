using FluentValidation;
using Service.DTOs;
using System;
using System.Text.RegularExpressions;

public class UserDTOValidation : AbstractValidator<UserDTO>
{
    public UserDTOValidation()
    {
        RuleFor(x => x.Username)
            .NotEmpty()
                .WithMessage("Kullanıcı adı gereklidir.")
                .WithErrorCode("USERNAME_REQUIRED")
                .WithState(_ => 400)
            .MinimumLength(3)
                .WithMessage("Kullanıcı adı en az 3 karakter olmalıdır.")
                .WithErrorCode("USERNAME_TOO_SHORT")
                .WithState(_ => 400)
            .MaximumLength(50)
                .WithMessage("Kullanıcı adı 50 karakterden fazla olamaz.")
                .WithErrorCode("USERNAME_TOO_LONG")
                .WithState(_ => 400)
            .Matches("^[a-zA-Z0-9_.-]*$")
                .WithMessage("Kullanıcı adı yalnızca harf, rakam, alt çizgi, nokta veya tire içerebilir.")
                .WithErrorCode("USERNAME_INVALID_CHARACTERS")
                .WithState(_ => 400);

        RuleFor(x => x.Email)
            .NotEmpty()
                .WithMessage("E-posta adresi gereklidir.")
                .WithErrorCode("EMAIL_REQUIRED")
                .WithState(_ => 400)
            .EmailAddress()
                .WithMessage("Geçerli bir e-posta adresi giriniz.")
                .WithErrorCode("EMAIL_INVALID_FORMAT")
                .WithState(_ => 400)
            .MaximumLength(100)
                .WithMessage("E-posta adresi 100 karakterden fazla olamaz.")
                .WithErrorCode("EMAIL_TOO_LONG")
                .WithState(_ => 400);

        RuleFor(x => x.Passwordhash)
            .NotEmpty()
                .WithMessage("Şifre özeti gereklidir.")
                .WithErrorCode("PASSWORDHASH_REQUIRED")
                .WithState(_ => 400);

        RuleFor(x => x.Firstname)
            .MaximumLength(50)
                .WithMessage("İsim 50 karakterden fazla olamaz.")
                .When(x => !string.IsNullOrEmpty(x.Firstname))
                .WithErrorCode("FIRSTNAME_TOO_LONG")
                .WithState(_ => 400)
            .Matches("^[a-zA-ZçÇğĞıİöÖşŞüÜ]*$")
                .WithMessage("İsim yalnızca harf içerebilir.")
                .When(x => !string.IsNullOrEmpty(x.Firstname))
                .WithErrorCode("FIRSTNAME_INVALID_CHARACTERS")
                .WithState(_ => 400);

        RuleFor(x => x.Lastname)
            .MaximumLength(50)
                .WithMessage("Soyisim 50 karakterden fazla olamaz.")
                .When(x => !string.IsNullOrEmpty(x.Lastname))
                .WithErrorCode("LASTNAME_TOO_LONG")
                .WithState(_ => 400)
            .Matches("^[a-zA-ZçÇğĞıİöÖşŞüÜ]*$")
                .WithMessage("Soyisim yalnızca harf içerebilir.")
                .When(x => !string.IsNullOrEmpty(x.Lastname))
                .WithErrorCode("LASTNAME_INVALID_CHARACTERS")
                .WithState(_ => 400);

        RuleFor(x => x.BirthDate)
            .Must(BeAValidDate)
                .WithMessage("Doğum tarihi geçerli bir tarih olmalıdır.")
                .When(x => x.BirthDate.HasValue)
                .WithErrorCode("BIRTHDATE_INVALID")
                .WithState(_ => 400)
            .Must(BeInThePast)
                .WithMessage("Doğum tarihi geçmiş bir tarih olmalıdır.")
                .When(x => x.BirthDate.HasValue)
                .WithErrorCode("BIRTHDATE_MUST_BE_IN_PAST")
                .WithState(_ => 400)
            .Must(BeAReasonableAge)
                .WithMessage("Kullanıcı en az 13 yaşında olmalıdır.")
                .When(x => x.BirthDate.HasValue)
                .WithErrorCode("USER_TOO_YOUNG")
                .WithState(_ => 400);
    }

    private bool BeAValidDate(DateTime? date)
    {
        return date.HasValue;
    }

    private bool BeInThePast(DateTime? date)
    {
        return date.HasValue && date.Value < DateTime.Now;
    }

    private bool BeAReasonableAge(DateTime? date)
    {
        if (!date.HasValue) return true;
        return date.Value <= DateTime.Now.AddYears(-13);
    }
}