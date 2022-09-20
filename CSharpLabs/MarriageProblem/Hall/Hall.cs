using CSharpLabs.MarriageProblem.ContenderGenerator;
using CSharpLabs.MarriageProblem.Exceptions;

namespace CSharpLabs.MarriageProblem.Hall;

public class Hall : IHall
{
    private readonly Contender.IContender[] _contenders;
    private readonly int _contenderNumber;
    
    public Hall(int contenderNumber, IContenderGenerator contenderGenerator)
    {
        _contenderNumber = contenderNumber;
        _contenders = contenderGenerator.GetContenders(contenderNumber);
    }

    public Contender.IContender this[int number]
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