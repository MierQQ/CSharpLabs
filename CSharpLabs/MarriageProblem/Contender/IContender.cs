namespace CSharpLabs.MarriageProblem.Contender;

public interface IContender
{
    public int Score { get; }

    public string Name { get; }

    public bool IsChecked { get; set; }
}