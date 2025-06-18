using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace WanderHub.Persistence;

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true)
            .AddJsonFile("appsettings.Development.json", optional: true)
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            // Fallback để xử lý tình huống không tìm thấy chuỗi kết nối
            connectionString = "Data Source=PHC\\PHC_1304;Initial Catalog=WanderHub;User id=sa;Password=Phc1304.,..; TrustServerCertificate=True";
        }

        optionsBuilder.UseSqlServer(connectionString);

        // Tạo interceptors nếu cần thiết
        // var convertDomainEventsInterceptor = new ConvertDomainEventsToOutboxMessagesInterceptor();
        // var updateAuditableEntitiesInterceptor = new UpdateAuditableEntitiesInterceptor();
        // optionsBuilder.AddInterceptors(convertDomainEventsInterceptor, updateAuditableEntitiesInterceptor);

        return new ApplicationDbContext(optionsBuilder.Options);
    }
}
