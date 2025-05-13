using Entity.Extension;
using Entity.Models;
using Entity.Result;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Repository.Contracts;
using Service.Contracts;
using Service.DTOs;
using Service.Validations;
using System.Security.Claims;

namespace Service;

public class CartManager(IRepositoryManager manager, IHttpContextAccessor httpContextAccessor) :ICartService
{
    private readonly IValidator<CartDTO> cartDTOValidation = new CartDTOValidation();
    private readonly IValidator<CartItemDTO> cartItemDTOValidation = new CartItemDTOValidation();
    private readonly IValidator<CartItemQuantityDTO> cartItemQuantityDTO = new CartItemQuantityDTOValidation();

    public Result<Cart> GetUserCart()
    {
        var httpUser = httpContextAccessor.HttpContext?.User ?? throw new UnauthorizedAccessException();
        var userId = httpUser.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new UnauthorizedAccessException();
        var cart = manager.Cart.GetByUserId(userId);
        if (cart == null) throw new ApiException("Cart not found.", "CART_NOT_FOUND", 404);
        return Result<Cart>.SuccessResult(cart, "Cart retrieved successfully.");
    }
    public Result<Cart> AddItemToCart(CartItemDTO cartItemDto)
    {
        var validation = cartItemDTOValidation.Validate(cartItemDto);
        if (!validation.IsValid)
        {
            var e = validation.Errors.First();
            throw new ApiException(e.ErrorMessage, e.ErrorCode!, (int)e.CustomState!);
        }

        var userId = httpContextAccessor.HttpContext?.User
                         .FindFirst(ClaimTypes.NameIdentifier)?.Value
                     ?? throw new UnauthorizedAccessException();

        var cart = manager.Cart.GetByUserId(userId);
        if (cart == null)
        {
            cart = new Cart()
            {
                Id = Guid.NewGuid().ToString(),
                Userid = userId,
            };
            manager.Cart.AddCart(cart);
        }

        var product = manager.Product.GetOneProduct(cartItemDto.ProductId);
        if (product==null) throw new ApiException("Product not found.", "PRODUCT_NOT_FOUND", 404);
        if (cartItemDto.Quantity > product.Stockquantity) throw new ApiException("Insufficient stock.", "INSUFFICIENT_STOCK", 400);

        var item= manager.CartItem.GetByCartIdAndProductId(cart.Id, cartItemDto.ProductId);
        if (item ==null)
        {
            item = new Cartitem
            {
                Id = Guid.NewGuid().ToString(),
                Cartid = cart.Id,
                Productid = cartItemDto.ProductId,
                Quantity = cartItemDto.Quantity,
            };
            manager.CartItem.AddCartItem(item);
            cart.Cartitems.Add(item);
        }
        else
        {
            item.Quantity += cartItemDto.Quantity;
            manager.CartItem.UpdateCartItem(item);
        }
        product.Stockquantity -= cartItemDto.Quantity;
        manager.Product.UpdateOneProduct(product);

        manager.Save();
        return Result<Cart>.SuccessResult(cart, "Product added to cart");
        //!BCrypt.Net.BCrypt.Verify("1234", databasedeki hashlenmiş veri) -> karşılaştırma
        //BCrypt.Net.BCrypt.HashPassword("1234") -> hashleyer databaseye atma
    }
    public Result<Cart> UpdateCartItemQuantity(string itemId, CartItemQuantityDTO cartItemQuantityDto)
    {
        var v = new CartItemQuantityDTOValidation().Validate(cartItemQuantityDto);
        if (!v.IsValid)
        {
            var e = v.Errors.First();
            throw new ApiException(e.ErrorMessage, e.ErrorCode!, (int)e.CustomState!);
        }

        var userId = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if(userId==null) throw new UnauthorizedAccessException();

        var cart = manager.Cart.GetByUserId(userId);
        if(cart==null) throw new ApiException("Cart not found", "CART_NOT_FOUND", 404);

        var item = cart.Cartitems.FirstOrDefault(c => c.Id == itemId);
        if (item == null) throw new ApiException("Item not found in cart", "ITEM_NOT_FOUND", 404);

        var product = manager.Product.GetOneProduct(item.Productid);
        if(product==null) throw new ApiException("Product not found", "PRODUCT_NOT_FOUND", 404);

        var stock = cartItemQuantityDto.Quantity- item.Quantity;
        if (stock > 0 && product.Stockquantity < stock)  throw new ApiException("Insufficient stock", "INSUFFICIENT_STOCK", 400);

        item.Quantity = cartItemQuantityDto.Quantity;
        manager.CartItem.UpdateCartItem(item);

        product.Stockquantity -= stock;
        manager.Product.UpdateOneProduct(product);
        manager.Save();
        return Result<Cart>.SuccessResult(cart, "Quantity updated");
    }

    public Result<Cart> DeleteCartItem(string itemId)
    {
        var userId = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null) throw new UnauthorizedAccessException();

        var cart = manager.Cart.GetByUserId(userId);
        if (cart == null) throw new ApiException("Cart not found", "CART_NOT_FOUND", 404);

        var item = cart.Cartitems.FirstOrDefault(ci => ci.Id == itemId);
        if (item == null) throw new ApiException("Item not found in cart", "ITEM_NOT_FOUND", 404);

        var product = manager.Product.GetOneProduct(item.Productid);
        if (product != null)
        {
            product.Stockquantity += item.Quantity;
            manager.Product.UpdateOneProduct(product);
        }

        manager.CartItem.DeleteCartItem(item);
        cart.Cartitems.Remove(item);

        manager.Save();
        return Result<Cart>.SuccessResult(cart, "Item removed from cart");
    }
}