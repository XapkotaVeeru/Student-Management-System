namespace StudentManagement.Domain.Entities;

public class SchoolClass
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string AcademicYear { get; set; }
    public bool IsActive { get; set; }
}