namespace CSharpLabs.MarriageProblem.Freind;

public interface IFreind
{
    public Contender.Contender? GetBestContender(Contender.Contender? contenderFirst, Contender.Contender? contenderSecond);
}