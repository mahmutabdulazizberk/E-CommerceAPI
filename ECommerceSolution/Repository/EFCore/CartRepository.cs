using Entity.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Context;
using Repository.Contracts;

namespace Repository.EFCore;

public class CartRepository(AppDbContext context) : RepositoryBase<Cart>(context), ICartRepository
{
    public Cart GetByUserId(string userId) =>
        FindByCondition(c => c.Userid == userId)
            .Include(c => c.Cartitems)
            .SingleOrDefault()!;
    public void AddCart(Cart cart) => Create(cart);
}