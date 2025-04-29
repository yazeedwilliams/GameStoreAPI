namespace GameStore.Api.DTOs;

public record class GameSummaryDTO(
    int ID,
    string Name,
    string Genre,
    decimal Price,
    DateOnly ReleaseDate);
