using CSharpLabs.MarriageProblem.Exceptions;

namespace CSharpLabs.MarriageProblem.Freind;

public class Freind : IFreind
{
    public Contender.IContender? GetBestContender(Contender.IContender? contenderFirst, Contender.IContender? contenderSecond)
    {
        if (contenderFirst == null && contenderSecond == null)
        {
            return null;
        }

        if (contenderFirst == null || contenderSecond == null)
        {
            return contenderFirst ?? contenderSecond;
        }

        if (!contenderFirst.IsChecked && !contenderSecond.IsChecked)
        {
            throw new MarriageProblemException("Two unknown contenders");
        }

        return contenderFirst.Score > contenderSecond.Score ? contenderFirst : contenderSecond;
    }
}