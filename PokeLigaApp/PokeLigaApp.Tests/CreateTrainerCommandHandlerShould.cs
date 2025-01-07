using FluentAssertions;

namespace PokeLigaApp.Tests;

public class CreateTrainerCommandHandlerShould
{
    [Test]
    public async Task create_trainer_with_a_username_and_a_list_of_pokemon()
    {
        const string username = "Ash";
        var pokemonTeam = new List<Pokemon>{};
        var createTrainerCommand = new CreateTrainerCommand(username, pokemonTeam);
        var testTrainerRepository = new TestTrainerRepository();
        
        var createTrainerCommandHandler = new CreateTrainerCommandHandler(testTrainerRepository);
        await createTrainerCommandHandler.Execute(createTrainerCommand);
        
        testTrainerRepository.Trainers.Should().ContainSingle(t => t.Username == username);
        testTrainerRepository.Trainers.Single().PokemonTeam.Should().BeEquivalentTo(pokemonTeam);
    }
}

public class TestTrainerRepository : TrainerRepository
{
    public List<Trainer> Trainers { get; set; } = new List<Trainer>();
    
    public async Task Add(Trainer trainer)
    {
        Trainers.Add(trainer);
    }
}

public interface TrainerRepository
{
    Task Add(Trainer trainer);
}

public class Trainer(string Username, List<Pokemon> PokemonTeam)
{
    public string Username { get; } = Username;
    public List<Pokemon> PokemonTeam { get; } = PokemonTeam;
}

public class CreateTrainerCommandHandler(TrainerRepository trainerRepository)
{
    public async Task Execute(CreateTrainerCommand command)
    {
        trainerRepository.Add(new Trainer(command.Username,command.PokemonTeam));
    }
}

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

public class Pokemon
{
}