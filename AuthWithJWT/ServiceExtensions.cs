using BAL;
using DAL;

namespace AuthWithJWT
{
    public static class ServiceExtensions
    {
        public static void RegisterRepos(this IServiceCollection services, IConfigurationManager configurationManager)
        {
            var ConnectinString = configurationManager["ConnectionStrings:ConnectionString"];

            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IUserRepo>(s => new UserRepo(ConnectinString));

        }
    }
}
