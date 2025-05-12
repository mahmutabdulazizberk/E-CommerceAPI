using Entity.Models;
using Repository.Context;
using Repository.Contracts;

namespace Repository.EFCore;

public class CartItemRepository(AppDbContext context) : RepositoryBase<Cartitem>(context), ICartItemRepository
{
    public Cartitem GetByCartIdAndProductId(string cartId, string productId) =>
        FindByCondition(ci => ci.Cartid == cartId && ci.Productid == productId)
            .SingleOrDefault()!;

    public void AddCartItem(Cartitem item) => Create(item);

    public void UpdateCartItem(Cartitem item) => Update(item);
    public void DeleteCartItem(Cartitem item) => Delete(item);
}