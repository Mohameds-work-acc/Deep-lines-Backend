using Deep_lines_Backend.BLL.Interfaces.IRepos;
using Deep_lines_Backend.BLL.Interfaces.IService;
using Deep_lines_Backend.BLL.JWT;
using Deep_lines_Backend.BLL.Services;
using Deep_lines_Backend.DAL.Repositories;
using System.Runtime.CompilerServices;

namespace Deep_lines_Backend.Extensions
{
    public static class ServiceLifetimesExtention
    {
        public static IServiceCollection addServiceLifetimesExtention(this IServiceCollection services)
        {

            services.AddScoped(typeof(IGenericRepo<>), typeof(GenericRepo<>));

            services.AddScoped<IBlogService, BlogService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProjectsService, ProjectsService>();
            services.AddScoped<IReviewService, ReviewService>();
            services.AddScoped<ISectorService, SectorService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IRefreshTokenRepo,RefreshTokenRepo>();
            services.AddScoped<IJWTService , JWTService>();
            services.AddScoped<IAuthService, AuthService>();
            return services;
        }
    }
}
