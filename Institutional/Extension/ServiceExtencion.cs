using DataAccessLayer;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using EntitiesLayer.Entities;
using EntitiesLayer.ModelDTO;
using Institutional.Localization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Localization;
using Service.Abstract;
using Service.Concrete;
using System.Reflection;


namespace Institutional
{
    public static class ServiceExtencion
    {
        public static void ConfiguratioSQLContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("Institutional")));
        }

        public static void ConfiguerRepostoryManager(this IServiceCollection services)
        {
            services.AddScoped<IGenericRepostory<UserEntites>, GenericRepostory<UserEntites>>();
            services.AddScoped<IGenericRepostory<AboutEntites>, GenericRepostory<AboutEntites>>();
            services.AddScoped<IGenericRepostory<ServicesEntities>, GenericRepostory<ServicesEntities>>();
            services.AddScoped<IGenericRepostory<ContactEntities>, GenericRepostory<ContactEntities>>();
            services.AddScoped<IGenericRepostory<CommentEntities>, GenericRepostory<CommentEntities>>();
            services.AddScoped<IGenericRepostory<CategoryEntities>, GenericRepostory<CategoryEntities>>();
            services.AddScoped<IGenericRepostory<ReferanceEntities>, GenericRepostory<ReferanceEntities>>();
            services.AddScoped<IGenericRepostory<PreferenceEntiites>, GenericRepostory<PreferenceEntiites>>();
            services.AddScoped<IGenericRepostory<ProjectEntities>, GenericRepostory<ProjectEntities>>();
            services.AddScoped<IGenericRepostory<ProductEntities>, GenericRepostory<ProductEntities>>();
            services.AddScoped<IGenericRepostory<ContactAdminEntities>, GenericRepostory<ContactAdminEntities>>();
            services.AddScoped<IRepostoryProducts, RepostoryProduct>();
            services.AddScoped<IRepostoryProjects, RepostoryProject>();
            services.AddScoped<IRepostoryCategory, RepostiryCategory>();
            services.AddScoped<IGenericRepostory<ContactAdminEntities>, GenericRepostory<ContactAdminEntities>>();
        }
        public static void ConfiguerServiceManager(this IServiceCollection services)
        {
            services.AddScoped<IGenericService<UserDTO>, GenericService<UserDTO>>();
            services.AddScoped<IGenericService<AboutDTO>, GenericService<AboutDTO>>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IContactAdminService, ContactAdminService>();
            services.AddScoped<IAboutService, AboutService>();
            services.AddScoped<IReferanceService, ReferanceService>();
            services.AddScoped<ICommandService, CommandService>();
            services.AddScoped<IContactService, ContactService>();
            services.AddScoped<IPreferanceService, PreferanceService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<IServiceService, ServiceService>();

        }
        public static void ConfigureIdentity(this IServiceCollection services)
        {
            var builder = services.AddIdentity<UserEntites, IdentityRole>
                (
                    opts =>
                    {
                        opts.Password.RequireDigit = true;
                        opts.Password.RequireLowercase = true;
                        opts.Password.RequireUppercase = true;
                        opts.Password.RequireNonAlphanumeric = true;
                        opts.Password.RequiredLength = 8;

                        opts.User.RequireUniqueEmail = true;

                    }
                ).AddEntityFrameworkStores<DataContext>()
                .AddDefaultTokenProviders();
        }

        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
