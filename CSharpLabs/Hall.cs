using CSharpLabs.Exceptions;

namespace CSharpLabs;

public class Hall : IHall
{
    private readonly Contender[] _contenders;
    
    public Hall(int contenderNumber, IContenderGenerator contenderGenerator)
    {
        _contenders = contenderGenerator.GetContenders(contenderNumber);
    }

    public Contender this[int number]
    {
        get
        {
            return _contenders[number];
        }
    }
}