namespace StudentManagement.Application.DTOs.Promotions;

public class PromoteStudentDto
{
    public int StudentId { get; set; }
    public int FromSchoolClassId { get; set; }
    public int ToSchoolClassId { get; set; }
    public int AcademicYear { get; set; }
}