using CSharpLabs.Exceptions;

namespace CSharpLabs;

public class DefaultContenderGenerator : IContenderGenerator
{
    private Random _random;
    
    private void Shuffle<T>(T[] array)
    {
        for (int i = array.Length - 1; i >= 1; i--)
        {
            int j = _random.Next(i + 1);
            (array[j], array[i]) = (array[i], array[j]);
        }
    }
    
    private string[] GetArrayFromRes(string filename, int length)
    {
        string[] lines = File.ReadAllLines(filename);
        if (lines.Length < length)
        {
            throw new MarriageProblemException($"file contents {lines.Length} lines, need {length}");
        }
        return lines;
    }
    
    public DefaultContenderGenerator()
    {
        _random = new Random();
    }
    public Contender[] GetContenders(int contenderNumber)
    {
        var names = GetArrayFromRes("names.txt", contenderNumber);
        var contenders = new Contender[contenderNumber];
        for (int i = 0; i < contenderNumber; i++)
        {
            contenders[i] = new Contender(i + 1, names[i]);
        }
        Shuffle(contenders);
        return contenders;
    }
}