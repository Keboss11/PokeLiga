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
    public void Test2()
    {
        var x = 2;
        var y = 2;
        
        var result = Sum(x, y);
        
        Assert.That(result, Is.EqualTo(4));
    }
}