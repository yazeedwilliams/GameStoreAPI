using GameStore.Api.DTOs;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

const string GetGameEndpointName = "GetGame";

List<GameDTO> games = [
    new (1, "Street Fighter II", "Fighting", 19.99M, new DateOnly(1992, 7, 15)),
    new (2, "Final Fantasy XIV", "Roleplaying", 59.99M, new DateOnly(2010, 9, 30)),
    new (3, "Fifa 23", "Sports", 69.99M, new DateOnly(2022, 9, 27))
    ];

// Get /games
app.MapGet("games", () => games);

// Get /games/1
app.MapGet("games/{ID}", (int id) => games.Find(game => game.ID == id)).WithName(GetGameEndpointName);

// Post /games
app.MapPost("games", (CreateGameDTO newGame) =>
{
    GameDTO game = new (
        games.Count + 1,
        newGame.Name,
        newGame.Genre,
        newGame.Price,
        newGame.ReleaseDate);
    
    games.Add(game);
    
    return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.ID}, game);
});

app.Run();
