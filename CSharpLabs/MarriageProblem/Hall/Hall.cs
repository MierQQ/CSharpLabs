using CSharpLabs.Exceptions;
using CSharpLabs.MarriageProblem.ContenderGenerator;

namespace CSharpLabs.MarriageProblem.Hall;

public class Hall : IHall
{
    private readonly Contender.Contender[] _contenders;
    private readonly int _contenderNumber;
    
    public Hall(int contenderNumber, IContenderGenerator contenderGenerator)
    {
        _contenderNumber = contenderNumber;
        _contenders = contenderGenerator.GetContenders(contenderNumber);
    }

    public Contender.Contender this[int number]
    {
        get
        {
            if (number < 0 || number >= _contenderNumber)
            {
                throw new MarriageProblemException("Out of range");
            }
            return _contenders[number];
        }
    }
}