using CSharpLabs.MarriageProblem.Exceptions;
using CSharpLabs.MarriageProblem.Web.Controllers.DTO;

namespace CSharpLabs.MarriageProblem.Princess;

public class PrincessWeb : IPrincessWeb
{
    private string? _maxContender;
    public bool IsChosenOne { get; private set; }

    private int _counter;

    public int Attempt { get; set; }

    public int Session { get; set; }

    public HttpClient Client { get; set; }

    private readonly int _threshold;
    private readonly int _contenderNumber;
    private readonly string _url;

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
        string postUrl = _url + "/freind/"+ Attempt +"/compare?session=" + Session + "&name1=" + first + "&name2=" + second;
        ContenderDto? dto = await (await Client!.PostAsync(postUrl, new StringContent(""))).Content.ReadFromJsonAsync<ContenderDto>();
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