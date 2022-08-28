using System;
using System.Text.Json.Serialization;

namespace DecisionHelper.API.Models
{
    public record PossibleAnswerDto(
        [property: JsonPropertyName("id")] Guid Id,
        [property: JsonPropertyName("answer")] string? Answer
    );
}
