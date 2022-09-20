namespace CSharpLabs.MarriageProblem.ContenderGenerator;

public interface IContenderGenerator
{
    public Contender.IContender[] GetContenders(int contenderNumber);
}