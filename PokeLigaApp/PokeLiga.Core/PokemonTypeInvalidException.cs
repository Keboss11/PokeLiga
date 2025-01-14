namespace PokeLiga.Core;

public class PokemonTypeInvalidException : Exception
{
    public PokemonTypeInvalidException(string message) : base(message){}
}