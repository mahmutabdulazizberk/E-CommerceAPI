using Entity.Models;
using Repository.Context;
using Repository.Contracts;

namespace Repository.EFCore;

public class UserRepository(AppDbContext context) : RepositoryBase<User>(context), IUserRepository
{
    public IQueryable<User> GetAllUsers() => FindAll().OrderBy(u => u.Id);
    public User GetOneUser(string id) => FindById(b => b.Id == id).SingleOrDefault()!; //singleordefault ile risk almak istemiyoruz.

    public User GetOneUserByUsername(string username) =>
        FindAll().FirstOrDefault(u => u.Username.ToLower() == username.ToLower());
    public void CreateOneUser(User user) => Create(user);
    public void UpdateOneUser(User user) => Update(user);
    public void DeleteOneUser(User user) => Delete(user);
}