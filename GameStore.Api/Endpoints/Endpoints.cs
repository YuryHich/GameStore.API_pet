using GameStore.Api.Dtos;

namespace GameStore.Api.Endpoints;

public static class Endpoints
{
    const string GetGameEndpointName = "GetGame";

    private static readonly List<GameDto> games = [
    new (
        1,
         "Street Fighter 11",
          "Figthing",
           19.19M, 
           new DateOnly(1992, 7, 15)),
    new (
        2,
        "Elden Ring",
        "Souls-like",
        20.00M,
        new DateOnly(2023, 4, 27 )),
    new (
        3,
        "God of War",
        "Action RPG",
        25.00M,
        new DateOnly(2021, 5, 28))
    ];

    public static void MapGamesEndpoints(this WebApplication app)
        {

        var group = app.MapGroup("/games");

            // GET /games
        group.MapGet("/", () => games);

        // GET /games/1
        group.MapGet("/{id}", (int id) =>{
            
            var game = games.Find(game => game.Id == id);

            return game is null ? Results.NotFound(): Results.Ok(games[id-1]);
            })
        .WithName(GetGameEndpointName);


        // POST /games
        group.MapPost("/", (CreateGameDto newGame) =>
        {
            GameDto game = new(
                games.Count+1,
                newGame.Name,
                newGame.Genre,
                newGame.Price,
                newGame.ReleaseDate
            );
            games.Add(game);

            return Results.CreatedAtRoute(GetGameEndpointName, new {id = game.Id}, game);
        });

        // PUT /games/1

        group.MapPut("/{id}", (int id, UpdateGameDto updatedGame) =>
        {
            var index = games.FindIndex(game => game.Id == id);

            if (index == -1) return Results.NotFound(); // better prectice to create resource instead of not found

            games[index] = new GameDto(
                id,
                updatedGame.Name,
                updatedGame.Genre,
                updatedGame.Price,
                updatedGame.ReleaseDate
            );
            return Results.NoContent();
        });


        // DELETE /games/1

        group.MapDelete("/{id}", (int id) =>
        {
            games.RemoveAll(game => game.Id == id);

            return Results.NoContent();
        });

        }

}
