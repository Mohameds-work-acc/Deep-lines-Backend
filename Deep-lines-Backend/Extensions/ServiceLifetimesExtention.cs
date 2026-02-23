using Deep_lines_Backend.BLL.Interfaces;
using Deep_lines_Backend.BLL.Interfaces.IService;
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
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<ICategoryService, CategoryService>();

            return services;
        }
    }
}
