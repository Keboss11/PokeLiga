namespace PokeLiga.Core;

public class CreateTrainerCommandHandler(TrainerRepository trainerRepository)
{
    public async Task Execute(CreateTrainerCommand command)
    {
        string[] validTypes = ["fuego", "agua", "planta"];
        foreach (var pokemon in command.PokemonTeam)
        {
            if (!validTypes.Contains(pokemon.Type.ToLower()))
            {
                throw new PokemonTypeInvalidException("Solo fuego, agua y planta son validos");
            }
        }
        trainerRepository.Add(new Trainer(command.Username,command.PokemonTeam));
    }
}