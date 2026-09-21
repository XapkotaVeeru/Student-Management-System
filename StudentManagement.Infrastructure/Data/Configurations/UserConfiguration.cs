using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentManagement.Domain.Entities;

namespace StudentManagement.Infrastructure.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> user)
    {
        user.HasKey(x => x.Id);
        
        user.Property(x => x.Username).IsRequired().HasMaxLength(50);
        user.HasIndex(x => x.Username).IsUnique();
        
        user.Property(x => x.Email).IsRequired().HasMaxLength(150);
        user.HasIndex(x => x.Email).IsUnique();
        
        user.Property(x => x.PasswordHash).IsRequired().HasMaxLength(255);
        
        user.Property(x=>x.UserRole).IsRequired().HasConversion<string>().HasMaxLength(50);
        
        user.Property(x => x.CreatedAt).IsRequired();
        
        user.Property(x => x.UpdatedAt).IsRequired();
        
        user.Property(x => x.IsActive).IsRequired();
    }
}