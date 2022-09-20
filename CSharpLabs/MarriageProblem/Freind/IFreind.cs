namespace CSharpLabs.MarriageProblem.Freind;

public interface IFreind
{
    public Contender.IContender? GetBestContender(Contender.IContender? contenderFirst, Contender.IContender? contenderSecond);
}