using Entity.Models;
using Entity.Result;
using Service.DTOs;

namespace Service.Contracts;

public interface ICartService
{
    Result<CartWithItemIdsDTO> GetUserCart();
    Result<Cart> AddItemToCart(CartItemDTO cartItemDto);
    Result<Cart> UpdateCartItemQuantity(string itemId, CartItemQuantityDTO cartItemQuantityDto); 
    Result<Cart> DeleteCartItem(string itemId);
}