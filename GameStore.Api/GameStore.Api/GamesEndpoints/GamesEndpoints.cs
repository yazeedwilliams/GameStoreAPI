using GameStore.Api.DTOs;

namespace GameStore.Api.GamesEndpoints;

public static class GamesEndpoints
{
    const string GetGameEndpointName = "GetGame";

    private static readonly List<GameDTO> games = [
        new (1, "Street Fighter II", "Fighting", 19.99M, new DateOnly(1992, 7, 15)),
        new (2, "Final Fantasy XIV", "Roleplaying", 59.99M, new DateOnly(2010, 9, 30)),
        new (3, "Fifa 23", "Sports", 69.99M, new DateOnly(2022, 9, 27))
    ];

    public static WebApplication MapGamesEndpoints(this WebApplication app)
    {
        // Get /games
        app.MapGet("games", () => games);

        // Get /games/1
        app.MapGet("games/{ID}", (int id) =>
        {
            GameDTO? game = games.Find(game => game.ID == id);

            return game is null ? Results.NotFound() : Results.Ok(game);

        }).WithName(GetGameEndpointName);

        // Post /games
        app.MapPost("games", (CreateGameDTO newGame) =>
        {
            GameDTO game = new(
                games.Count + 1,
                newGame.Name,
                newGame.Genre,
                newGame.Price,
                newGame.ReleaseDate);

            games.Add(game);

            return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.ID }, game);
        });

        // Put
        app.MapPut("games/{ID}", (int id, UpdateGameDTO updatedGame) =>
        {
            var index = games.FindIndex(game => game.ID == id);

            if (index == -1)
            {
                return Results.NotFound();
            }

            games[index] = new GameDTO(
                id,
                updatedGame.Name,
                updatedGame.Genre,
                updatedGame.Price,
                updatedGame.ReleaseDate
            );

            return Results.NoContent();
        });

        // Delete /games/1
        app.MapDelete("games/{ID}", (int id) =>
        {
            games.RemoveAll(game => game.ID == id);

            return Results.NoContent();
        });

        return app;

    }
}
