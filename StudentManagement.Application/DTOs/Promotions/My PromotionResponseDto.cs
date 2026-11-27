using StudentManagement.Domain.Enums;

namespace StudentManagement.Application.DTOs.Promotions;

public class MyPromotionResponseDto
{
    public int Id { get; set; }
    public int FromSchoolClassId { get; set; }
    public string FromClassName { get; set; } = string.Empty;
    public int ToSchoolClassId { get; set; }
    public string ToClassName { get; set; } = string.Empty;
    public int AcademicYear { get; set; }
    public PromotionStatus Status { get; set; }
    public DateTime PromotionDate { get; set; }
}