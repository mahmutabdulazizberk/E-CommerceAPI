using Entity.Models;

namespace Repository.Contracts;

public interface ICartRepository : IRepositoryBase<Cart>
{
    IQueryable<Cart> GetAllCarts();
    Cart GetOneCart(string id);
    public Cart GetByUserId(string userId);
    void AddCart(Cart cart);
    void UpdateItemCart(Cart cart);
    void DeleteCartItem(Cartitem item);
}