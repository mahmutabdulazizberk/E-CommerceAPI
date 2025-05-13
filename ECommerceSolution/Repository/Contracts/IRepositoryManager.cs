namespace Repository.Contracts;

public interface IRepositoryManager
{
    ICategoryRepository Category { get; }
    IUserRepository User { get; }
    IProductRepository Product { get; }
    ICartRepository Cart { get; }
    ICartItemRepository CartItem { get; }
    IOrderRepository Order { get; }
    IOrderItemRepository OrderItem { get; }
    void Save();
}