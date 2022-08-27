using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace GhostUI.Models
{
    public record DecisionDto(
        [property: JsonPropertyName("id")] Guid Id,
        [property: JsonPropertyName("question")] string? Question,
        [property: JsonPropertyName("result")] string? Result,
        [property: JsonPropertyName("possibleAnswers")] IReadOnlyList<PossibleAnswerDto> PossibleAnswers
    );
}
