using Entity.Models;
using Entity.Result;
using Service.DTOs;

namespace Service.Contracts;

public interface ICartService
{
    Result<Cart> GetUserCart();
    Result<Cart> AddItemToCart(CartItemDTO cartItemDto); //kullanıyoruz 2. fonkisyon
    Result<Cart> UpdateCartItemQuantity(string itemId, CartItemQuantityDTO cartItemQuantityDto); //kullanıyoruz 3. fonksiyon
    Result<Cart> DeleteCartItem(string itemId); //kullanıyoruz 4. fonksiyon
}