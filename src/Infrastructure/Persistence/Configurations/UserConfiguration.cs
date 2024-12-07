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
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasMany(u => u.Products)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.ManufactureEmail) 
                .HasPrincipalKey(u => u.Email)          
                .OnDelete(DeleteBehavior.Cascade);      
        }
    }
}
