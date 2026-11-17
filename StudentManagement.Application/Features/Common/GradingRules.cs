namespace StudentManagement.Application.Features.Common;

public static class GradingRules
{
    public const decimal PassPercentage = 40m;

    public static bool HasPassed(decimal marksObtained, decimal maxMarks)
        => maxMarks > 0 && marksObtained / maxMarks * 100 >= PassPercentage;
}
