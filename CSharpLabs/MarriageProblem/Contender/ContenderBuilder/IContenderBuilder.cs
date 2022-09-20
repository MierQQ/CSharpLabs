namespace CSharpLabs.MarriageProblem.Contender.ContenderBuilder;

public interface IContenderBuilder
{
    public IContender GetContender(int score, string name);
}