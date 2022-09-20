namespace CSharpLabs.MarriageProblem.Princess;

public interface IPrincess
{
    public bool IsChosenOne { get; }
    public void ConsiderContender(Contender.IContender contender);
    public Contender.IContender? GetHusband();
}