using Entity.Models;

namespace Repository.Contracts;

public interface ICartRepository : IRepositoryBase<Cart>
{
    public Cart GetByUserId(string userId); //kullanılıyor
    void AddCart(Cart cart);
}