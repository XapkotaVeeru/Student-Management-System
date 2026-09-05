using StudentManagement.Domain.Enums;

namespace StudentManagement.Domain.Entities;

public class StudentPromotion
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public int FromSchoolClassId { get; set; }
    public int ToSchoolClassId { get; set; }
    public int AcademicYear { get; set; }
    public PromotionStatus Status { get; set; }
    public int ApprovedByUserId { get; set; }
    public DateTime PromotionDate { get; set; }
}
