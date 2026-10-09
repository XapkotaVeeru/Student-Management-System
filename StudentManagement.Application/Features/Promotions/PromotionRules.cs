using StudentManagement.Domain.Entities;

namespace StudentManagement.Application.Features.Promotions;

public static class PromotionRules
{
    public static bool HasPassedAll(IEnumerable<ExamMark> examMarks, decimal passPercentage)
    {
        var effectiveMarks = examMarks
            .GroupBy(x => new {x.ExamId, x.SubjectId})
            .Select(g => g.OrderByDescending(x => x.IsReExam).First())
            .ToList();
        
        if(effectiveMarks.Count == 0)
        {
            return false;
        }
        
        return effectiveMarks.All(x => 
            x.MaxMarks > 0 && 
            x.MarksObtained/x.MaxMarks * 100 >= passPercentage);
        
    }
}