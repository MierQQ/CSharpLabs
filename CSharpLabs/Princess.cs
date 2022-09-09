using CSharpLabs.Exceptions;

namespace CSharpLabs;

public class Princess : IPrincess
{
    private Contender? _maxContender;
    private bool _isChosenOne;
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

    public void ConsiderContender(Contender? contender)
    {
        _counter++;
        _maxContender = _freind.GetBestContender(_maxContender, contender);
        _isChosenOne = _counter > _threshold && _maxContender == contender;
        if (_counter == _contenderNumber)
        {
            _maxContender = contender;
            _isChosenOne = true;
        }
    }

    public bool IsChosenOne()
    {
        return _isChosenOne;
    }

    public Contender? GetHusband()
    {
        if (_isChosenOne)
        {
            return _maxContender;
        }
        return null;
    }
}