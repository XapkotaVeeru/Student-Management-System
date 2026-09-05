namespace StudentManagement.Application.DTOs.Common;

public class ResponseDto<T>
{
    public bool Status { get; set; }
    public string Message { get; set; } = string.Empty;
    public T?  Data { get; set; }
}