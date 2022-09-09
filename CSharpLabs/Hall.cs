using CSharpLabs.Exceptions;

namespace CSharpLabs;

public class Hall
{
    private readonly Contender?[] _contenders;

    private readonly Random _random;

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

    public Hall(int contenderNumber)
    {
        _random = new Random();
        var names = GetArrayFromRes("names.txt", contenderNumber);
        _contenders = new Contender?[contenderNumber];
        for (int i = 0; i < 100; i++)
        {
            _contenders[i] = new Contender(i + 1, names[i]);
        }
        Shuffle(_contenders);
    }

    public Hall(string filename, int contenderNumber)
    {
        _random = new Random();
        string[] lines = GetArrayFromRes(filename, contenderNumber);
        _contenders = new Contender?[contenderNumber];
        for (int i = 0; i < 100; i++)
        {
            string[] lineWords = lines[i].Split(":");
            _contenders[i] = new Contender(int.Parse(lineWords[1]), lineWords[0]);
        }
    }

    public Contender? Get(int number)
    {
        return _contenders[number];
    }
}