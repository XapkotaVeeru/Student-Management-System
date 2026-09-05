namespace StudentManagement.Application.DTOs.Marks;

public class UpdateMarkDto
{
    public int StudentId { get; set; } 
    public int SubjectId { get; set; }
    public decimal MarksObtained { get; set; }
    public decimal MaxMarks { get; set; }
}