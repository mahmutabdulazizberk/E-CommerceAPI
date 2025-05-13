using Repository.Context;
using Repository.Contracts;

namespace Repository.EFCore
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly AppDbContext _context;
        private readonly Lazy<ICategoryRepository> _categoryRepository;
        private readonly Lazy<IUserRepository> _userRepository;
        private readonly Lazy<IProductRepository> _productRepository;
        private readonly Lazy<ICartRepository> _cartRepository;
        private readonly Lazy<ICartItemRepository> _cartItemRepository;
        private readonly Lazy<IOrderRepository> _orderRepository;
        private readonly Lazy<IOrderItemRepository> _orderItemRepository;
        public RepositoryManager(AppDbContext context)
        {
            _context = context;
            _categoryRepository = new Lazy<ICategoryRepository>(() => new CategoryRepository(_context));
            _userRepository = new Lazy<IUserRepository>(() => new UserRepository(_context));
            _productRepository = new Lazy<IProductRepository>(() => new ProductRepository(_context));
            _cartRepository = new Lazy<ICartRepository>(() => new CartRepository(_context));
            _cartItemRepository= new Lazy<ICartItemRepository>(() => new CartItemRepository(_context));
            _orderRepository = new Lazy<IOrderRepository>(() => new OrderRepository(_context));
            _orderItemRepository = new Lazy<IOrderItemRepository>(() => new OrderItemRepository(_context));
        }
        public ICategoryRepository Category => _categoryRepository.Value;
        public IUserRepository User => _userRepository.Value;
        public IProductRepository Product => _productRepository.Value;
        public ICartRepository Cart => _cartRepository.Value;
        public ICartItemRepository CartItem => _cartItemRepository.Value;
        public IOrderRepository Order => _orderRepository.Value;
        public IOrderItemRepository OrderItem => _orderItemRepository.Value;

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
