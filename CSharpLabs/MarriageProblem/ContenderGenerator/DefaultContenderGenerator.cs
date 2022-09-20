using CSharpLabs.MarriageProblem.Contender.ContenderBuilder;
using CSharpLabs.MarriageProblem.Exceptions;

namespace CSharpLabs.MarriageProblem.ContenderGenerator;

public class DefaultContenderGenerator : IContenderGenerator
{
    private readonly Random _random;
    private readonly IContenderBuilder _contenderBuilder;

    private void Shuffle<T>(T[] array)
    {
        for (var i = array.Length - 1; i >= 1; i--)
        {
            var j = _random.Next(i + 1);
            (array[j], array[i]) = (array[i], array[j]);
        }
    }
    
    private string[] GetArrayFromRes(string filename, int length)
    {
        var lines = File.ReadAllLines(filename);
        if (lines.Length < length)
        {
            throw new MarriageProblemException($"file contents {lines.Length} lines, need {length}");
        }
        return lines;
    }
    
    public DefaultContenderGenerator(IContenderBuilder contenderBuilder)
    {
        _contenderBuilder = contenderBuilder;
        _random = new Random();
    }
    public Contender.IContender[] GetContenders(int contenderNumber)
    {
        var names = GetArrayFromRes("names.txt", contenderNumber);
        var contenders = new Contender.IContender[contenderNumber];
        for (var i = 0; i < contenderNumber; i++)
        {
            contenders[i] = _contenderBuilder.GetContender(i + 1, names[i]);
        }
        Shuffle(contenders);
        return contenders;
    }
}