namespace CSharpLabs;

public class Freind
{
    public Contender? GetBestContender(Contender? contenderFirst, Contender? contenderSecond)
    {
        if (contenderFirst == null && contenderSecond == null)
        {
            return null;
        }

        if (contenderFirst == null || contenderSecond == null)
        {
            return contenderFirst ?? contenderSecond;
        }
        
        return contenderFirst.GetScore() > contenderSecond.GetScore() ? contenderFirst : contenderSecond;
    }
}