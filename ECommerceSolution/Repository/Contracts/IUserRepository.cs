using Entity.Models;

namespace Repository.Contracts;

public interface IUserRepository: IRepositoryBase<User>
{
    IQueryable<User> GetAllUsers();
    User GetOneUser(string id);
    User GetOneUserByUsername(string username);
    void CreateOneUser(User user);
    void UpdateOneUser(User user);
    void DeleteOneUser(User user);
}