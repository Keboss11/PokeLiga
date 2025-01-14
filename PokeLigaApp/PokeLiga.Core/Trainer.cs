namespace PokeLiga.Core;

public class Trainer(string Username, List<Pokemon> PokemonTeam)
{
    public string Username { get; } = Username;
    public List<Pokemon> PokemonTeam { get; } = PokemonTeam;
}