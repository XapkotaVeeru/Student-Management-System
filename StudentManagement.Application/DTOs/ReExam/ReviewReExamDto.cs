using StudentManagement.Domain.Enums;

namespace StudentManagement.Application.DTOs.ReExam;

public class ReviewReExamDto
{
    public ReExamStatus Status { get; set; }
    public string Reason { get; set; } = string.Empty;
}