namespace GameStore.Api.Models;

public class Game
{
    public int Id {get; set;}

    public required string Name {get; set;}

    public Genre? Genre {get; set;}
}
