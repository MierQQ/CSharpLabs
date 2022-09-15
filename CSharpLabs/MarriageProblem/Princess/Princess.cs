using CSharpLabs.Exceptions;
using CSharpLabs.MarriageProblem.Freind;

namespace CSharpLabs.MarriageProblem.Princess;

public class Princess : IPrincess
{
    private Contender.Contender? _maxContender;
    private bool _isChosenOne;
    public bool IsChosenOne => _isChosenOne;
    private int _counter;
    private readonly int _threshold;
    private readonly int _contenderNumber;
    private readonly IFreind _freind;
    

    public Princess(IFreind freind, int contenderNumber, int threshold)
    {
        _isChosenOne = false;
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

    public void ConsiderContender(Contender.Contender contender)
    {
        if (_counter++ >= _contenderNumber)
        {
            throw new MarriageProblemException("Out of range");
        }
        _maxContender = _freind.GetBestContender(_maxContender, contender);
        _isChosenOne = _counter > _threshold && _maxContender == contender;
        if (_counter == _contenderNumber)
        {
            _maxContender = contender;
            _isChosenOne = true;
        }
        contender.IsChecked = true;
    }

    public Contender.Contender? GetHusband()
    {
        if (_isChosenOne)
        {
            return _maxContender;
        }
        return null;
    }
}