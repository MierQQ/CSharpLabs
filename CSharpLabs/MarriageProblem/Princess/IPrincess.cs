namespace CSharpLabs.MarriageProblem.Princess;

public interface IPrincess
{
    public bool IsChosenOne { get; }
    public void ConsiderContender(Contender.Contender contender);
    public Contender.Contender? GetHusband();
}