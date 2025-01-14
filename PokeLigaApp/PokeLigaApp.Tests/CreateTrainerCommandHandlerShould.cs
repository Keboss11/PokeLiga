using FluentAssertions;
using PokeLiga.Core;

namespace PokeLigaApp.Tests;

public class CreateTrainerCommandHandlerShould
{
    [Test]
    public async Task create_trainer_with_a_username_and_a_list_of_pokemon()
    {
        const string username = "Ash";
        var pokemonTeam = new List<Pokemon>{ new Pokemon("Charmander","Fuego"), new Pokemon("squirtle","agua"),new Pokemon("bulbasaur", "planta")};
        var createTrainerCommand = new CreateTrainerCommand(username, pokemonTeam);
        var testTrainerRepository = new TestTrainerRepository();
        
        var createTrainerCommandHandler = new CreateTrainerCommandHandler(testTrainerRepository);
        await createTrainerCommandHandler.Execute(createTrainerCommand);
        
        testTrainerRepository.Trainers.Should().ContainSingle(t => t.Username == username);
        testTrainerRepository.Trainers.Single().PokemonTeam.Should().BeEquivalentTo(pokemonTeam);
    }

    [Test]
    public async Task raise_an_error_when_a_pokemon_type_is_invalid()
    {
        const string username = "Ash";
        var pokemonTeam = new List<Pokemon>{new Pokemon("Pikachu", "Electrico")};
        var createTrainerCommand = new CreateTrainerCommand(username, pokemonTeam);
        var testTrainerRepository = new TestTrainerRepository();
        
        var createTrainerCommandHandler = new CreateTrainerCommandHandler(testTrainerRepository);
        Func<Task> handler = async() => await createTrainerCommandHandler.Execute(createTrainerCommand);
        
        await handler.Should().ThrowAsync<PokemonTypeInvalidException>().WithMessage("Solo fuego, agua y planta son validos");

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