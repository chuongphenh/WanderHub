using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WanderHub.Domain.Entities;
using WanderHub.Persistence.Constants;

namespace WanderHub.Persistence.Configurations;

internal sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable(TableNames.Product);

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).HasMaxLength(200).IsRequired(true);
        builder.Property(x => x.Description)
            .HasMaxLength(250).IsRequired(true);
    }
}
