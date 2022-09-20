namespace CSharpLabs.MarriageProblem.Contender;

public class Contender : IContender
{
    public int Score { get; }

    public string Name { get; }

    public bool IsChecked { get; set; }

    public Contender(int score, string name)
    {
        IsChecked = false;
        Score = score;
        Name = name;
    }
}