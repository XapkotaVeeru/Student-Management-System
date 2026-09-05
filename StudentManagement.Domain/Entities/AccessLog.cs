namespace StudentManagement.Domain.Entities;

public class AccessLog
{
    public int Id { get; set; }
    public int? UserId { get; set; }
    public string HttpMethod { get; set; } = string.Empty;
    public string Endpoint { get; set; } = string.Empty;
    public int StatusCode { get; set; }
    public string? IpAddress { get; set; }
    public DateTime Timestamp { get; set; }
}