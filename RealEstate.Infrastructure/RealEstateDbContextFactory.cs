using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace RealEstate.Infrastructure
{
    public class RealEstateDbContextFactory : IDesignTimeDbContextFactory<RealEstateDbContext>
    {
        public RealEstateDbContext CreateDbContext(string[] args)
        {
            var projectPath = Directory.GetCurrentDirectory();
            var mainProjectPath = Path.Combine(projectPath, "../RealEstate.API");

            var configuration = new ConfigurationBuilder()
                .SetBasePath(mainProjectPath)
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<RealEstateDbContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            optionsBuilder.UseSqlServer(connectionString);

            return new RealEstateDbContext(optionsBuilder.Options);
        }
    }
}
