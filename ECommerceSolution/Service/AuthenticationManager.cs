using Entity.Models;
using Entity.Result;
using Microsoft.IdentityModel.Tokens;
using Repository.Contracts;
using Service.Contracts;
using Service.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Entity.Extension;
using FluentValidation;

namespace Service;

public class AuthenticationManager(IRepositoryManager manager, AuthenticationDTO authenticationDTO) : IAuthenticationService
{
    private readonly IValidator<UserDTO> userDTOValidation = new UserDTOValidation();
    public String CreateOneAuthentication(LoginDTO loginDto)
    {
        string CreateToken(User user)
        {
            if (authenticationDTO.Key == null)
                throw new ApiException("Authentication Key cannot be empty!", "JWT_KEY_CANNOT_BE_EMPTY");

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationDTO.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claimDizisi = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
            };

            var token = new JwtSecurityToken(
                authenticationDTO.Issuer,
                authenticationDTO.Audience,
                claimDizisi,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        
        User Authenticate(LoginDTO loginDto)
        {
            var userEntity = manager.User.GetOneUserByUsername(loginDto.Username);
            if (userEntity == null) throw new ApiException($"User with Username '{loginDto.Username}' not found.", "USERNAME_NOT_FOUND", 404);
            if (!BCrypt.Net.BCrypt.Verify(loginDto.Password, userEntity.Passwordhash)) throw new ApiException("Password is Incorrect", "USER_NOT_FOUND", 401);
            return userEntity;
        }

        return CreateToken(Authenticate(loginDto));
    }
    public Result<User> CreateOneUser(UserDTO userDto)
    {
        var validationResult = userDTOValidation.ValidateAsync(userDto);
        if (!validationResult.Result.IsValid)
        {
            var error = validationResult.Result.Errors.First();
            throw new ApiException(error.ErrorMessage, error.ErrorCode, (int)error.CustomState);
        }

        var user = new User()
        {
            Id = Guid.NewGuid().ToString(),
            Username = userDto.Username,
            Email = userDto.Email,
            Passwordhash = BCrypt.Net.BCrypt.HashPassword(userDto.Passwordhash),
            Firstname = userDto.Firstname,
            Lastname = userDto.Lastname,
            BirthDate = userDto.BirthDate,
        };
        manager.User.CreateOneUser(user);
        manager.Save();
        return Result<User>.SuccessResult(user, $"User '{user.Id}' created successfully.");
    }
}