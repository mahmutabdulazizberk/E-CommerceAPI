using Entity.Models;
using Entity.Result;
using Service.DTOs;

namespace Service.Contracts;

public interface IUserService
{
    Result<IEnumerable<User>> GetAllUsers();
    Result<User> GetOneUser();
    Result<User> UpdateOneUser(UserDTO userDto);
    Result<User> DeleteOneUser();
}