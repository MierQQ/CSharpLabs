using CSharpLabs.Exceptions;

namespace CSharpLabs.MarriageProblem.Freind;

public class Freind : IFreind
{
    public Contender.Contender? GetBestContender(Contender.Contender? contenderFirst, Contender.Contender? contenderSecond)
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
            throw new MarriageProblemException("Comparation of two unknown contenders");
        }
        
        return contenderFirst.Score > contenderSecond.Score ? contenderFirst : contenderSecond;
    }
}