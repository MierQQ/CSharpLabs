using CSharpLabs.MarriageProblem.Exceptions;
using CSharpLabs.MarriageProblem.Freind;

namespace CSharpLabs.MarriageProblem.Princess;

public class Princess : IPrincess
{
    private Contender.IContender? _maxContender;
    public bool IsChosenOne { get; private set; }

    private int _counter;
    private readonly int _threshold;
    private readonly int _contenderNumber;
    private readonly IFreind _freind;
    

    public Princess(IFreind freind, int contenderNumber, int threshold)
    {
        IsChosenOne = false;
        if (contenderNumber <= threshold)
        {
            throw new MarriageProblemException("Wrong threshold");
        }
        _contenderNumber = contenderNumber;
        _threshold = threshold;
        _maxContender = null;
        _counter = 0;
        _freind = freind;
    }

    public void ConsiderContender(Contender.IContender contender)
    {
        if (_counter++ >= _contenderNumber)
        {
            throw new MarriageProblemException("Out of range");
        }
        _maxContender = _freind.GetBestContender(_maxContender, contender);
        IsChosenOne = _counter > _threshold && _maxContender == contender;
        if (_counter == _contenderNumber)
        {
            _maxContender = contender;
            IsChosenOne = true;
        }
        contender.IsChecked = true;
    }

    public Contender.IContender? GetHusband()
    {
        if (IsChosenOne)
        {
            return _maxContender;
        }
        return null;
    }
}