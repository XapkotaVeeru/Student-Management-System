using StudentManagement.Domain.Enums;

namespace StudentManagement.Application.DTOs.Promotions;

public class PromotionResponseDto
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public int FromSchoolClassId { get; set; }
    public int ToSchoolClassId { get; set; }
    public int AcademicYear { get; set; }
    public DateTime PromotionDate { get; set; }
    public PromotionStatus Status { get; set; }
    public int ApprovedByUserId { get; set; }
}