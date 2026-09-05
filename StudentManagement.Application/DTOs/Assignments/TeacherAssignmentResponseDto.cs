namespace StudentManagement.Application.DTOs.Assignments;

public class TeacherAssignmentResponseDto
{
    public int Id { get; set; }
    public int TeacherId { get; set; }
    public int SchoolClassId { get; set; }
    public int SubjectId { get; set; }
}