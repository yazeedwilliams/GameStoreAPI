using System;
using GameStore.Api.DTOs;
using GameStore.Api.Entities;

namespace GameStore.Api.Mapping;

public static class GameMapping
{
    public static Game ToEntity(this CreateGameDTO game)
    {
        return new Game()
        {
            Name = game.Name,
            GenreID = game.GenreID,
            Price = game.Price,
            ReleaseDate = game.ReleaseDate
        };
    }

    public static GameDTO ToDTO(this Game game)
    {
        return new(
               game.ID,
               game.Name,
               game.Genre!.Name,
               game.Price,
               game.ReleaseDate
        );
    }
}
