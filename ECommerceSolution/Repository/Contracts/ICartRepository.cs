using Entity.Models;

namespace Repository.Contracts;

public interface ICartRepository : IRepositoryBase<Cart>
{
    public Cart GetByUserId(string userId); //kullanılıyor
    public IList<string> GetCartItemIdsByCartId(string cartId);
    void AddCart(Cart cart);
}