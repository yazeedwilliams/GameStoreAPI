using System.ComponentModel.DataAnnotations;

namespace GameStore.Api.DTOs;

public record class UpdateGameDTO(
    [Required][StringLength(50)] string Name,
    int GenreID,
    [Range(1, 100)] decimal Price,
    DateOnly ReleaseDate
);
