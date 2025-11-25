using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace EFCore.Data
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);

            var contextType = typeof(ApplicationDbContext);
            var constructors = contextType.GetConstructors();
            var constructor = constructors.First(c =>
            {
                var parameters = c.GetParameters();
                return parameters.Length == 1 &&
                       parameters[0].ParameterType == typeof(DbContextOptions<ApplicationDbContext>);
            });

            return (ApplicationDbContext)constructor.Invoke(new object[] { optionsBuilder.Options });
        }
    }
}
