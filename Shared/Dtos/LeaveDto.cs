namespace Shared.Dtos;

public record LeaveDto(int Id,int PersonelId,LeaveStatus Status, DateOnly StartDate,DateOnly EndDateTime );