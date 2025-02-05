using System.Text.Json;
using PokeLiga.Core;

namespace PokeLigaApp;



public class JsonTrainerRepository : TrainerRepository
{
    private readonly string _filePath;

    public JsonTrainerRepository()
    {
        _filePath = "C:/mateo/JsonTrainers.json";
    }
    public JsonTrainerRepository(string filePath)
    {
        _filePath = filePath;
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


