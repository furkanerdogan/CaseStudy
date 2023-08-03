using DataAccess.Concrete.EntityFramewrok.Context;
using Microsoft.EntityFrameworkCore;

namespace InfosetPizzaShipmentAPI.Extensions
{
    public static class ServicesExtensions
    {
        public static void ConfigureSqlContext(this IServiceCollection services,
            IConfiguration configuration) => services.AddDbContext<MyDbContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("sqlConnection")));
    }
}
