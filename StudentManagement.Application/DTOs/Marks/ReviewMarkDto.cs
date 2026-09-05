namespace StudentManagement.Application.DTOs.Marks;

public class ReviewMarkDto
{
    public bool IsApproved { get; set; }
    public string Comment { get; set; } = string.Empty;
}