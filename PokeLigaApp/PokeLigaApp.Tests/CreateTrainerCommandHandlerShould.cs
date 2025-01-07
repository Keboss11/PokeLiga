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

public class Trainer
{
    public string Username { get; set; }
    public List<Pokemon> PokemonTeam { get; set; }
}

public class CreateTrainerCommandHandler(TestTrainerRepository testTrainerRepository)
{
    public async Task Execute(CreateTrainerCommand command)
    {
        
    }
}

public class CreateTrainerCommand
{
    public CreateTrainerCommand(string username, List<Pokemon> pokemonTeam)
    {
        
    }
}

public class Pokemon
{
}