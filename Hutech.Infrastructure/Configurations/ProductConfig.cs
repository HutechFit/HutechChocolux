using Hutech.Domain.Entities;
using Hutech.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hutech.Infrastructure.Configurations;

public class ProductConfig : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .UseIdentityColumn();

        builder.Property(x => x.Name)
            .HasMaxLength(50)
            .IsUnicode()
            .IsRequired();

        builder.Property(x => x.Price)
            .IsRequired();

        builder.Property(x => x.Status)
            .HasDefaultValue(Status.InStock)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(250)
            .IsUnicode();

        builder.Property(x => x.Image);

        builder.HasOne(x => x.Category)
            .WithMany(x => x.Products)
            .HasForeignKey(x => x.CategoryId);
    }
}