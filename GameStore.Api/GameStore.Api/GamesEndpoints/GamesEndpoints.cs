using GameStore.Api.Data;
using GameStore.Api.DTOs;
using GameStore.Api.Entities;
using GameStore.Api.Mapping;

namespace GameStore.Api.GamesEndpoints;

public static class GamesEndpoints
{
    const string GetGameEndpointName = "GetGame";

    private static readonly List<GameSummaryDTO> games = [
        new (1, "Street Fighter II", "Fighting", 19.99M, new DateOnly(1992, 7, 15)),
        new (2, "Final Fantasy XIV", "Roleplaying", 59.99M, new DateOnly(2010, 9, 30)),
        new (3, "Fifa 23", "Sports", 69.99M, new DateOnly(2022, 9, 27))
    ];

    public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("games").WithParameterValidation();

        // Get /games
        group.MapGet("/", () => games);

        // Get /games/1
        group.MapGet("/{ID}", (int id, GameStoreContext dbContext) =>
        {
            Game? game = dbContext.Games.Find(id);

            return game is null ? Results.NotFound() : Results.Ok(game.ToGameDetailsDTO());

        }).WithName(GetGameEndpointName);

        // Post /games
        group.MapPost("/", (CreateGameDTO newGame, GameStoreContext dbContext) =>
        {
            Game game = newGame.ToEntity();

            dbContext.Games.Add(game);
            dbContext.SaveChanges();


            return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.ID }, game.ToGameDetailsDTO());
        }).WithParameterValidation();

        // Put
        group.MapPut("/{ID}", (int id, UpdateGameDTO updatedGame, GameStoreContext dbContext) =>
        {
            var existingGame = dbContext.Games.Find(id);

            if (existingGame is null)
            {
                return Results.NotFound();
            }

            dbContext.Entry(existingGame).CurrentValues.SetValues(updatedGame.ToEntity(id));
            dbContext.SaveChanges();

            return Results.NoContent();
        });

        // Delete /games/1
        group.MapDelete("/{ID}", (int id) =>
        {
            games.RemoveAll(game => game.ID == id);

            return Results.NoContent();
        });

        return group;
    }
}
