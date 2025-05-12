namespace Service.Contracts;

public interface IServiceManager
{
    ICategoryService CategoryService { get; }
    IUserService UserService { get; }
    IProductService ProductService { get; }
    ICartService CartService { get; }
    IAuthenticationService AuthenticationService { get; }
}