using StudentManagement.Application.DTOs.Common;
using StudentManagement.Application.DTOs.Promotions;

namespace StudentManagement.Application.Interfaces;

public interface IPromotionService
{
    Task<ResponseDto<IEnumerable<PromotionResponseDto>>> ListPromotionAsync();
    
    Task<ResponseDto<PromotionResponseDto>> ReadAsync(int id);

    Task<ResponseDto<PromotionResponseDto>> PromoteAsync(int id, PromoteStudentDto student);

    Task<ResponseDto<PromotionResponseDto>> ReviewAsync(int id, ReviewPromotionDto student);
}