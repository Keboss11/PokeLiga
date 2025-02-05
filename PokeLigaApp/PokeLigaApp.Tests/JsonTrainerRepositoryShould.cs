using System.Text.Json;
using FluentAssertions;
using PokeLiga.Core;

namespace PokeLigaApp.Tests;

public class JsonTrainerRepositoryShould
{
    [SetUp]
    public async Task SetUp()
    {
        if (File.Exists("testTrainer.json"))
        {
            File.WriteAllText("testTrainer.json", string.Empty);
        }
        else
        {
            await using var jsonFile = File.Create("testTrainer.json"); // Crear un archivo vacío si no existe
        }      
    }
    
    [Test]
    public async Task save_trainer_correctly_in_json()
    {
        const string username = "Ash";
        var pokemonTeam = new List<Pokemon>{new Pokemon("Charmander", "Fuego")};
        

          
        
        var TrainerRepository = new JsonTrainerRepository();
        

        var trainer = new Trainer(username, pokemonTeam);
        await TrainerRepository.Add(trainer);
        
        

        string jsonContent = await File.ReadAllTextAsync("testTrainer.json");

        var entrenadores = JsonSerializer.Deserialize<List<Trainer>>(jsonContent, new JsonSerializerOptions { WriteIndented = true });
        
        entrenadores.Should().HaveCount(1);
        
        entrenadores[0].Username.Should().Be(username);
        entrenadores[0].PokemonTeam.Should().BeEquivalentTo(pokemonTeam);
        
    }
}