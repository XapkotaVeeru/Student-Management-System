namespace StudentManagement.Application.DTOs.Assignments;

public class TeacherAssignmentListItemDto
{
    public int Id { get; set; }
    public int TeacherId { get; set; }
    public string TeacherName { get; set; } = string.Empty;
    public string EmployeeNumber { get; set; } = string.Empty;
    public int SchoolClassId { get; set; }
    public string SchoolClassName { get; set; } = string.Empty;
    public int SubjectId { get; set; }
    public string SubjectName { get; set; } = string.Empty;
}