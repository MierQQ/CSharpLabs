namespace CSharpLabs.MarriageProblem.Contender.ContenderBuilder;

public class ContenderBuilder : IContenderBuilder
{
    public IContender GetContender(int score, string name)
    {
        return new Contender(score, name);
    }
}