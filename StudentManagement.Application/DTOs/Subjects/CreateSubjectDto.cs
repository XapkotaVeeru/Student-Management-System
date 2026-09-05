namespace StudentManagement.Application.DTOs.Subjects;

public class CreateSubjectDto
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string SubjectCode { get; set; } = string.Empty;
}