namespace StudentManagement.Domain.Entities;

public class MarkApproval
{
    public int Id { get; set; }
    public int ExamMarkId { get; set; }
    public int ApprovedByUserId { get; set; }
    public bool IsApproved { get; set; }
    public string Comment { get; set; } = string.Empty;
    public DateTime ActionDate { get; set; }
}