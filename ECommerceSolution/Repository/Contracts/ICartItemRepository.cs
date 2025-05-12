using Entity.Models;

namespace Repository.Contracts;

public interface ICartItemRepository
{
    Cartitem GetByCartIdAndProductId(string cartId, string productId);
    void AddCartItem(Cartitem item);
    void UpdateCartItem(Cartitem item);
    void DeleteCartItem(Cartitem item);
}