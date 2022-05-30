using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PickPoint.Test.Common.Data;

namespace PickPoint.Test.Infrastructure.EntityTypeConfigurations
{
    public class OrderEntityTypeConfiguration : EntityTypeConfiguration<Domain.Order>
    {
        public override void Configure(EntityTypeBuilder<Domain.Order> builder)
        {

            builder.Property(x=>x.Status);
            builder.Property(x => x.RecipientPhoneNumber);
            builder.Property(x => x.Price);
            builder.Property(x => x.Items);
            builder.Property(x => x.Recipient);
            builder.HasOne(i => i.Postamat).WithMany(i => i.Orders);
            base.Configure(builder);

        }
    }
}
