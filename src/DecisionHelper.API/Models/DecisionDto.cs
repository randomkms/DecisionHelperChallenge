namespace DecisionHelper.API.Models
{
    public record DecisionDto(
        Guid Id,
        string? Question,
        string? Result,
        IReadOnlyList<PossibleAnswerDto> PossibleAnswers
    );
}
