using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentManagement.Domain.Entities;

namespace StudentManagement.Infrastructure.Data.Configurations;

public class ExamMarkConfiguration : IEntityTypeConfiguration<ExamMark>
{
    public void Configure(EntityTypeBuilder<ExamMark> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.MarksObtained)
            .IsRequired()
            .HasPrecision(5, 2);

        builder.Property(x => x.MaxMarks)
            .IsRequired()
            .HasPrecision(5, 2);

        builder.Property(x => x.EnteredOn)
            .IsRequired();

        builder.Property(x => x.UpdatedOn)
            .IsRequired();

        builder.Property(x => x.Status)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(20);
        
        builder.HasOne<Exam>()
            .WithMany()
            .HasForeignKey(x => x.ExamId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne<Student>()
            .WithMany()
            .HasForeignKey(x => x.StudentId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne<Subject>()
            .WithMany()
            .HasForeignKey(x => x.SubjectId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne<Teacher>()
            .WithMany()
            .HasForeignKey(x => x.EnteredByTeacherId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => new
            {
                x.ExamId,
                x.StudentId,
                x.SubjectId
            })
            .IsUnique();
    }
}