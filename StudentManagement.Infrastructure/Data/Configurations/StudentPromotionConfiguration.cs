using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentManagement.Domain.Entities;

namespace StudentManagement.Infrastructure.Data.Configurations;

public class StudentPromotionConfiguration
    : IEntityTypeConfiguration<StudentPromotion>
{
    public void Configure(EntityTypeBuilder<StudentPromotion> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.AcademicYear)
            .IsRequired();

        builder.Property(x => x.Status)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.Property(x => x.PromotionDate)
            .IsRequired();
        
        builder.HasOne<Student>()
            .WithMany()
            .HasForeignKey(x => x.StudentId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne<SchoolClass>()
            .WithMany()
            .HasForeignKey(x => x.FromSchoolClassId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne<SchoolClass>()
            .WithMany()
            .HasForeignKey(x => x.ToSchoolClassId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(x => x.ReviewedByUserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}