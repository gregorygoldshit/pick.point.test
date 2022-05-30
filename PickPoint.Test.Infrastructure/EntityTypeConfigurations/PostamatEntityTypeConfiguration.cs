using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PickPoint.Test.Common.Data;

namespace PickPoint.Test.Infrastructure.EntityTypeConfigurations
{
    public class PostamatEntityTypeConfiguration : EntityTypeConfiguration<Domain.Postamat>
    {
        public override void Configure(EntityTypeBuilder<Domain.Postamat> builder)
        {
            builder.Property(x => x.Address);
            builder.Property(x => x.IsActive);
            base.Configure(builder);
        }
    }
}
