using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentManagement.Domain.Entities;

namespace StudentManagement.Infrastructure.Data.Configurations;

public class MarkApprovalConfiguration : IEntityTypeConfiguration<MarkApproval>
{
    public void Configure(EntityTypeBuilder<MarkApproval> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.IsApproved)
            .IsRequired();

        builder.Property(x => x.Comment)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(x => x.ActionDate)
            .IsRequired();

        builder.HasOne<ExamMark>()
            .WithMany()
            .HasForeignKey(x => x.ExamMarkId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(x => x.ApprovedByUserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}