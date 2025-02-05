using FluentAssertions;
using PokeLiga.Core;
using System.Text.Json;

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


public class JsonTrainerRepository : TrainerRepository
{
    private readonly string _filePath;

    public JsonTrainerRepository()
    {
        _filePath = "testTrainer.json";
    }

    public async Task Add(Trainer trainer)
    {
        // Read existing trainers from the file
        var trainers = await LoadTrainersAsync();

        // Add the new trainer
        trainers.Add(trainer);

        // Save the updated list back to the file
        await SaveTrainersAsync(trainers);
    }

    private async Task<List<Trainer>> LoadTrainersAsync()
    {
        try
        {
            if (!File.Exists(_filePath))
            {
                // Return an empty list if the file doesn't exist
                return new List<Trainer>();
            }

            string json = await File.ReadAllTextAsync(_filePath);

            // Deserialize the JSON into a list of trainers
            return JsonSerializer.Deserialize<List<Trainer>>(json) ?? new List<Trainer>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading trainers: {ex.Message}");
            return new List<Trainer>();
        }
    }

    private async Task SaveTrainersAsync(List<Trainer> trainers)
    {
        try
        {
            // Serialize the list of trainers to JSON
            string json = JsonSerializer.Serialize(trainers, new JsonSerializerOptions { WriteIndented = true });

            // Write the JSON to the file
            await File.WriteAllTextAsync(_filePath, json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving trainers: {ex.Message}");
        }
    }
}