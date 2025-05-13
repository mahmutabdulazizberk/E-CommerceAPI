using Entity.Models;

namespace Repository.Contracts;

public interface ICartItemRepository
{
    Cartitem GetByCartIdAndProductId(string cartId, string productId); //kullanılıyor
    void AddCartItem(Cartitem item); //kullanılıyor
    void UpdateCartItem(Cartitem item);
    void DeleteCartItem(Cartitem item);
}