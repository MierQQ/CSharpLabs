using CSharpLabs.MarriageProblem.Contender;
using CSharpLabs.MarriageProblem.ContenderGenerator;

namespace MarriageProblemTests.Mocks;

public class MockContenderGenerator : IContenderGenerator
{
    public delegate Contender Index2Contender(int index);

    private readonly Index2Contender _index2Contender;
    public MockContenderGenerator(Index2Contender index2Contender)
    {
        _index2Contender = index2Contender;
    }
    public Contender[] GetContenders(int contenderNumber)
    {
        var contender = new Contender[contenderNumber];
        for (var i = 0; i < contenderNumber; ++i)
        {
            contender[i] = _index2Contender(i);
        }
        return contender;
    }
}