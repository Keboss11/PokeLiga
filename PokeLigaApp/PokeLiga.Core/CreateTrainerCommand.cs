namespace PokeLiga.Core;

public class CreateTrainerCommand
{
    public string Username { get; }
    public List<Pokemon> PokemonTeam { get; }

    public CreateTrainerCommand(string username, List<Pokemon> pokemonTeam)
    {
        Username = username;
        PokemonTeam = pokemonTeam;
    }
}