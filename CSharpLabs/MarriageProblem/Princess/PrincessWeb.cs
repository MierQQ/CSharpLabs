using CSharpLabs.MarriageProblem.Exceptions;
using CSharpLabs.MarriageProblem.Web.Controllers;

namespace CSharpLabs.MarriageProblem.Princess;

public class PrincessWeb : IPrincessWeb
{
    private string? _maxContender;
    public bool IsChosenOne { get; private set; }

    private int _counter;

    public int Attempt
    {
        set { _attempt = value; }
    }

    private int _attempt;
    public int Session
    {
        set { _session = value; }
    }

    private int _session;

    public HttpClient Client
    {
        set { _client = value; }
    }

    private HttpClient _client;

    private readonly int _threshold;
    private readonly int _contenderNumber;
    private string _url;

    public PrincessWeb(int contenderNumber, int threshold, string url)
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
        _url = url;
    }

    public void ConsiderContender(string contender)
    {
        if (_counter++ >= _contenderNumber)
        {
            throw new MarriageProblemException("Out of range");
        }

        if (_maxContender == null)
        {
            _maxContender = contender;
        }
        _maxContender = AskFreind(_maxContender, contender).Result;
        IsChosenOne = _counter > _threshold && _maxContender == contender;
        if (_counter == _contenderNumber)
        {
            _maxContender = contender;
            IsChosenOne = true;
        }
    }

    public async Task<string> AskFreind(string first, string second)
    {
        string postUrl = _url + "/freind/"+ _attempt +"/compare?session=" + _session + "&name1=" + first + "&name2=" + second;
        ContenderDTO? dto = await (await _client.PostAsync(postUrl, new StringContent(""))).Content.ReadFromJsonAsync<ContenderDTO>();
        if (dto is null)
        {
            throw new MarriageProblemException("compare error");
        }
        if (dto.Text is null)
        {
            throw new MarriageProblemException("freind send null");
        }
        return dto.Text;
    }

    public string? GetHusband()
    {
        if (IsChosenOne)
        {
            return _maxContender;
        }
        return null;
    }
}