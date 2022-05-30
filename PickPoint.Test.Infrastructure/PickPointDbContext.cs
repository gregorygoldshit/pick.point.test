using Microsoft.EntityFrameworkCore;
using PickPoint.Test.Infrastructure.EntityTypeConfigurations;

namespace PickPoint.Test.Infrastructure;

public class PickPointDbContext : DbContext
{
    public PickPointDbContext()
    {

    }

    public PickPointDbContext(DbContextOptions options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new OrderEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new PostamatEntityTypeConfiguration());
    }

    //dotnet ef migrations add initial_create --project PickPoint.Test.Infrastructure --startup-project PickPoint.Test.Api
    //dotnet ef database update --project PickPoint.Test.Infrastructure --startup-project PickPoint.Test.Api
}
