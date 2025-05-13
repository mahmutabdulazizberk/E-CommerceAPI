using Service.Contracts;
using Repository.Contracts;
using Microsoft.Extensions.Options;
using Service.DTOs;
using Microsoft.AspNetCore.Http;

namespace Service
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<ICategoryService> _categoryService;
        private readonly Lazy<IUserService> _userService;
        private readonly Lazy<IProductService> _productService;
        private readonly Lazy<ICartService> _cartService;
        private readonly Lazy<IAuthenticationService> _authenticationService;
        private readonly Lazy<IOrderService> _orderService;

        public ServiceManager(IRepositoryManager repositoryManager, IOptions<AuthenticationDTO> jwtSettingsOptions, IHttpContextAccessor httpContextAccessor)
        {
            _categoryService = new Lazy<ICategoryService>(() => new CategoryManager(repositoryManager));
            _userService = new Lazy<IUserService>(() => new UserManager(repositoryManager, httpContextAccessor));
            _productService = new Lazy<IProductService>(() => new ProductManager(repositoryManager));
            _cartService = new Lazy<ICartService>(() => new CartManager(repositoryManager, httpContextAccessor));
            _authenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationManager(repositoryManager, jwtSettingsOptions.Value));
            _orderService = new Lazy<IOrderService>(() => new OrderManager(repositoryManager, httpContextAccessor));
        }

        public ICategoryService CategoryService => _categoryService.Value;
        public IUserService UserService => _userService.Value;
        public IProductService ProductService => _productService.Value;
        public ICartService CartService => _cartService.Value;
        public IAuthenticationService AuthenticationService => _authenticationService.Value;
        public IOrderService OrderService => _orderService.Value;
    }
}