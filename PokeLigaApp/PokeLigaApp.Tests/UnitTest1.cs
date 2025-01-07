using NUnit.Framework.Constraints;

namespace PokeLigaApp.Tests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    private int Sum(int x, int y)
    {
        return x + y;
    }
    
    

    [Test]
    public void Test1()
    {
        var x = 1;
        var y = 2;
        
        var result = Sum(x, y);
        
        Assert.That(result, Is.EqualTo(3));
    }
    
     

    [Test]
    public void fire_pokemon_beats_plant_pokemon()
    {
        var pokemon1 = "fuego";
        var pokemon2 = "planta";

        var result = Combat(pokemon1, pokemon2);
        
        Assert.That(result, Is.EqualTo("fuego"));
    }
    
    [Test]
    public void fire_pokemon_beats_plant_pokemon2()
    {
        var pokemon1 = "agua";
        var pokemon2 = "planta";

        var result = Combat(pokemon1, pokemon2);
        
        Assert.That(result, Is.EqualTo("planta"));
    }

    private string Combat(string pokemon1, string pokemon2)
    {
        var winner = "";

        if (pokemon1 == "fuego")
        {
            winner = "fuego";
        } else if (pokemon1 == "agua")
        {
            winner = "planta";
        }
        
        return winner;
    }
}