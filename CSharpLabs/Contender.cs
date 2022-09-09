namespace CSharpLabs;

public class Contender
{
    private readonly int _score;
    private readonly string _name;

    public Contender(int score, string name)
    {
        _score = score;
        _name = name;
    }

    public int GetScore()
    {
        return _score;
    }

    public string GetName()
    {
        return _name;
    }
}