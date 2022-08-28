namespace DecisionHelper.API.Models
{
    public record DecisionNodeDto(
        Guid Id,
        string? Question,
        string? Answer,
        string? Result,
        IReadOnlyList<DecisionNodeDto> Children
    );
}
