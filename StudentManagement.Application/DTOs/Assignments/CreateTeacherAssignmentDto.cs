namespace StudentManagement.Application.DTOs.Assignments;

public class CreateTeacherAssignmentDto
{
    public int TeacherId { get; set; }
    public int SchoolClassId { get; set; }
    public int SubjectId { get; set; }
}