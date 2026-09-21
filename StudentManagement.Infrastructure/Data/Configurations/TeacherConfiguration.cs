using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentManagement.Domain.Entities;

namespace StudentManagement.Infrastructure.Data.Configurations;

public class TeacherConfiguration : IEntityTypeConfiguration<Teacher>
{
    public void Configure(EntityTypeBuilder<Teacher> teacher)
    {
        teacher.HasKey(x => x.Id);
        
        teacher.Property(x=>x.Gender).HasConversion<String>().IsRequired().HasMaxLength(50);
        
        teacher.Property(x=> x.FirstName).IsRequired().HasMaxLength(50);
        
        teacher.Property(x=> x.LastName).IsRequired().HasMaxLength(50);
        
        teacher.Property(x=> x.Email).IsRequired().HasMaxLength(150);
        teacher.HasIndex(x=>x.Email).IsUnique();
        
        teacher.Property(x => x.PhoneNumber).IsRequired().HasMaxLength(50);
        teacher.HasIndex(x => x.PhoneNumber).IsUnique();
        
        teacher.Property(x => x.IsActive).IsRequired().HasDefaultValue(true);
        
        teacher.Property(x => x.EmployeeNumber).IsRequired().HasMaxLength(50);
        teacher.HasIndex(x => x.EmployeeNumber).IsUnique();
        
        
        teacher.HasOne<User>().WithOne().HasForeignKey<Teacher>(x => x.UserId);
        
        teacher.HasMany<TeacherAssignment>().WithOne().HasForeignKey(x => x.TeacherId);
        
    }
}