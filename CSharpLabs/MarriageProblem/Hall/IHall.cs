namespace CSharpLabs.MarriageProblem.Hall;

public interface IHall
{
    public Contender.IContender this[int number] { get;}
}