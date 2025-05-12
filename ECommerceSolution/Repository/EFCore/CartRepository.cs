using Entity.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Context;
using Repository.Contracts;

namespace Repository.EFCore;

public class CartRepository(AppDbContext context) : RepositoryBase<Cart>(context), ICartRepository
{
    public IQueryable<Cart> GetAllCarts() => FindAll().OrderBy(b => b.Id);
    public Cart GetOneCart(string id) => FindById(b => b.Id == id).SingleOrDefault()!; //singleordefault ile risk almak istemiyoruz.
    public Cart GetByUserId(string userId) =>
        FindByCondition(c => c.Userid == userId)
            .Include(c => c.Cartitems)
            .SingleOrDefault()!;
    public void AddCart(Cart cart) => Create(cart);
    public void UpdateItemCart(Cart cart) => Update(cart);
    public void DeleteCartItem(Cartitem item) => Delete(item);

}