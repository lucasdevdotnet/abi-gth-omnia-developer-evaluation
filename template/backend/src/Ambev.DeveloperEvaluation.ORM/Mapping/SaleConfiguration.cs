using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class SaleConfiguration : IEntityTypeConfiguration<Sale>
{
    public void Configure(EntityTypeBuilder<Sale> builder)
    {
        builder.ToTable("Sales");

        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id).HasColumnType("uuid");

        builder.Property(s => s.Number).IsRequired().HasMaxLength(50);
        builder.Property(s => s.SaleDate).IsRequired();
        builder.Property(s => s.CustomerId).IsRequired();
        builder.Property(s => s.CustomerName).IsRequired().HasMaxLength(100);
        builder.Property(s => s.BranchId).IsRequired();
        builder.Property(s => s.BranchName).IsRequired().HasMaxLength(100);
        builder.Property(s => s.Cancelled).IsRequired();

        builder.Ignore(s => s.TotalAmount);
        builder.HasMany(s => s.Items)
               .WithOne()
               .HasForeignKey("SaleId")
               .OnDelete(DeleteBehavior.Cascade);
    }
}
