namespace StudentManagement.Domain.Entities;

public class SchoolClass
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int AcademicYear { get; set; }
    public bool IsActive { get; set; }
}