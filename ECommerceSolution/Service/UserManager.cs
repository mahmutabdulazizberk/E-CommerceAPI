using System.Security.Claims;
using Entity.Extension;
using Entity.Models;
using Entity.Result;
using FluentValidation;
using Repository.Contracts;
using Service.Contracts;
using Service.DTOs;
using Microsoft.AspNetCore.Http;

namespace Service;

public class UserManager(IRepositoryManager manager) : IUserService
{
    private readonly IValidator<UserDTO> userDTOValidation= new UserDTOValidation();
    private readonly IHttpContextAccessor httpContextAccessor= new HttpContextAccessor();
    public Result<IEnumerable<User>> GetAllUsers()
    {
        IEnumerable<User> users = manager.User.GetAllUsers();
        if (users == null) throw new ApiException("User not found in the database.", "USER_NOT_FOUND", 404);
        return Result<IEnumerable<User>>.SuccessResult(users, "Users fetched successfully.");
    }

    public Result<User> GetOneUser()
    {
        var httpUser= httpContextAccessor.HttpContext?.User ?? throw new UnauthorizedAccessException();
        var userId = httpUser.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new UnauthorizedAccessException();
        var user = manager.User.GetOneUser(userId);
        if (user == null) throw new ApiException($"User not found.", "USER_NOT_FOUND", 404);
        return Result<User>.SuccessResult(user, $"User retrieved successfully.");
    }

    public Result<User> UpdateOneUser(UserDTO userDto)
    {
        var validationResult = userDTOValidation.ValidateAsync(userDto);
        if (!validationResult.Result.IsValid)
        {
            var error = validationResult.Result.Errors.First();
            throw new ApiException(error.ErrorMessage, error.ErrorCode, (int)error.CustomState);
        }
        var httpUser = httpContextAccessor.HttpContext?.User ?? throw new UnauthorizedAccessException();
        var userId = httpUser.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new UnauthorizedAccessException();
        var entity = manager.User.GetOneUser(userId);
        entity.Username = userDto.Username;
        entity.Email = userDto.Email;
        entity.Passwordhash = userDto.Passwordhash;
        entity.Firstname = userDto.Firstname;
        entity.Lastname = userDto.Lastname;
        entity.BirthDate = userDto.BirthDate;

        manager.User.Update(entity);
        manager.Save();
        return Result<User>.SuccessResult(entity, $"User '{entity.Id}' update successfully.");
    }

    public Result<User> DeleteOneUser()
    {
        var httpUser = httpContextAccessor.HttpContext?.User ?? throw new UnauthorizedAccessException();
        var userId = httpUser.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new UnauthorizedAccessException();
        var entity = manager.User.GetOneUser(userId);
        if (entity == null) throw new ApiException($"User with ID '{userId}' not found.", "USER_NOT_FOUND", 404);

        manager.User.DeleteOneUser(entity);
        manager.Save();
        return Result<User>.SuccessResult(entity, $"User '{entity.Id}' delete successfully.");
    }

}