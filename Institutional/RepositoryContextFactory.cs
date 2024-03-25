using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer;

namespace Institutional
{
        public class RepositoryContextFactory : IDesignTimeDbContextFactory<DataContext>
        {
        public DataContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<DataContext>()
                .UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                prj => prj.MigrationsAssembly("Institutional"));

            return new DataContext(builder.Options);
        }
    }
  
}
