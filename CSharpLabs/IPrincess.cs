namespace CSharpLabs;

public interface IPrincess
{
    public void ConsiderContender(Contender? contender);
    public bool IsChosenOne();
    public Contender? GetHusband();
}