namespace CSharpLabs.MarriageProblem.Contender;

public class Contender
{
    private readonly int _score;
    public int Score => _score;
    
    private readonly string _name;
    public string Name => _name;
    
    private bool _isChecked;
    public bool IsChecked
    {
        get => _isChecked;
        set => _isChecked = value;
    }

    public Contender(int score, string name)
    {
        _isChecked = false;
        _score = score;
        _name = name;
    }
}