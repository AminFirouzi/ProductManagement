using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            // Composite primary key
            builder.HasKey(p => new { p.ManufactureEmail, p.ProduceDate });

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.ManufacturePhone)
                .HasMaxLength(50);

            builder.Property(p => p.ManufactureEmail)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.IsAvailable)
                .IsRequired();

            // Relationship with User
            builder.HasOne(p => p.User)
                .WithMany(u => u.Products)
                .HasForeignKey(p => p.ManufactureEmail)
                .HasPrincipalKey(u => u.Email)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
