namespace StudentManagement.Domain.Entities;

public class TeacherAssignment
{
    public int Id { get; set; }
    public int TeacherId { get; set; }
    public int SchoolClassId { get; set; }
    public int SubjectId { get; set; }
    
}