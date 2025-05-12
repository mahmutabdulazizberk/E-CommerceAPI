using Entity.Models;
using Entity.Result;
using Service.DTOs;

namespace Service.Contracts;

public interface IAuthenticationService
{
    String CreateOneAuthentication(LoginDTO loginDto);
    Result<User> CreateOneUser(UserDTO userDto);
}