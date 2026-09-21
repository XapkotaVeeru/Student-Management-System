using System.Runtime.InteropServices.JavaScript;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentManagement.Domain.Entities;

namespace StudentManagement.Infrastructure.Data.Configurations;

public class StudentConfiguration : IEntityTypeConfiguration<Student>
{ 
        public void Configure(EntityTypeBuilder<Student> student)
        {
                student.HasKey(x => x.Id);
                
                student.Property(x => x.StudentNumber).HasMaxLength(50).IsRequired();
                student.HasIndex(x => x.StudentNumber).IsUnique();
                
                student.Property(x => x.FirstName).HasMaxLength(50).IsRequired();
                
                student.Property(x => x.LastName).HasMaxLength(50).IsRequired();
                
                student.Property(x => x.PhoneNumber).HasMaxLength(50).IsRequired();
                
                student.Property(x => x.Email).IsRequired().HasMaxLength(150);
                student.HasIndex(x => x.Email).IsUnique();
                
                student.Property(x => x.DateOfBirth).IsRequired().HasColumnType("date");
                
                student.Property(x => x.IsActive).HasDefaultValue(true).IsRequired();
                
                student.Property(x => x.EnrollmentDate).IsRequired();
                
                student.Property(x => x.SchoolClassId).IsRequired();
                
                student.Property(x => x.AdmissionNumber).IsRequired().HasMaxLength(50);
                
                student.Property(x => x.Address).IsRequired().HasMaxLength(150);

                student.Property(x => x.Gender).HasConversion<string>().IsRequired().HasMaxLength(50);
                
                
                student.HasOne<User>().WithOne().HasForeignKey<Student>(x => x.UserId)
                        .OnDelete(DeleteBehavior.Restrict);
                
                student.HasOne<SchoolClass>().WithMany().HasForeignKey(x => x.SchoolClassId)
                        .OnDelete(DeleteBehavior.Restrict);

        }
    
}