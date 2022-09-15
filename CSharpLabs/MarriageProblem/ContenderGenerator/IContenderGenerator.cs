namespace CSharpLabs.MarriageProblem.ContenderGenerator;

public interface IContenderGenerator
{
    public Contender.Contender[] GetContenders(int contenderNumber);
}