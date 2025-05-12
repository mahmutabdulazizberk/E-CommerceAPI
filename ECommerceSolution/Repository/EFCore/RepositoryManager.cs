using Entity.Models;
using Repository.Context;
using Repository.Contracts;
using Repository.EFCore;

namespace Repository.EFCore
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly AppDbContext _context;
        private readonly Lazy<ICategoryRepository> _categoryRepository;
        private readonly Lazy<IUserRepository> _userRepository;
        private readonly Lazy<IProductRepository> _productRepository;
        private readonly Lazy<ICartRepository> _cartRepository;

        public RepositoryManager(AppDbContext context)
        {
            _context = context;
            _categoryRepository = new Lazy<ICategoryRepository>(() => new CategoryRepository(_context));
            _userRepository = new Lazy<IUserRepository>(() => new UserRepository(_context));
            _productRepository = new Lazy<IProductRepository>(() => new ProductRepository(_context));
            _cartRepository = new Lazy<ICartRepository>(() => new CartRepository(_context));
        }
        public ICategoryRepository Category => _categoryRepository.Value;
        public IUserRepository User => _userRepository.Value;
        public IProductRepository Product => _productRepository.Value;
        public ICartRepository Cart => _cartRepository.Value;

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
