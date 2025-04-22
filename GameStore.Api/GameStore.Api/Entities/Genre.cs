using System;
using Microsoft.AspNetCore.SignalR;

namespace GameStore.Api.Entities;

public class Genre
{
    public int ID { get; set; }

    public required string Name { get; set; }
}
