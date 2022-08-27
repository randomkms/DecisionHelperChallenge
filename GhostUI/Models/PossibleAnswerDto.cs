using System;
using System.Text.Json.Serialization;

namespace GhostUI.Models
{
    public record PossibleAnswerDto(
        [property: JsonPropertyName("id")] Guid Id,
        [property: JsonPropertyName("answer")] string? Answer
    );
}
