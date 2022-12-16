using CSharpLabs.MarriageProblem.Princess;
using CSharpLabs.MarriageProblem.Exceptions;
using CSharpLabs.MarriageProblem.Web.Controllers.DTO;

namespace CSharpLabs.MarriageProblem;

public class MarriageProblemWeb : IHostedService
{
    private readonly int _contenderNumber;
    private readonly IPrincessWeb _princess;
    private int _attempt;
    private readonly int _session;
    private readonly string _url;

    public MarriageProblemWeb(int contenderNumber, IPrincessWeb princess, int attempt, string url, int session)
    {
        _contenderNumber = contenderNumber;
        _princess = princess;
        _attempt = attempt;
        _session = session;
        _url = url;
    }

    private async Task<int> SolveProblem()
    {
        int score;
        using (HttpClient client = new HttpClient())
        {
            string postResetUrl = _url + "/hall/"+ _attempt +"/reset?session=" + _session;
            if (_attempt == 0)
            {
                postResetUrl = _url + "/hall/reset?session=" + _session;
            }
            AttemptIdDto? attemptIdDto = await (await client.PostAsync(postResetUrl, new StringContent(""))).Content.ReadFromJsonAsync<AttemptIdDto>();
            if (attemptIdDto is null)
            {
                throw new MarriageProblemException("reset error");
            }
            _attempt = attemptIdDto.Id;

            string? contender;
            var i = 0;
            _princess.Attempt = _attempt;
            _princess.Client = client;
            _princess.Session = _session;
            do
            {
                string postUrl = _url + "/hall/"+ _attempt +"/next?session=" + _session;
                ContenderDto? dto = await (await client.PostAsync(postUrl, new StringContent(""))).Content.ReadFromJsonAsync<ContenderDto>();
                if (dto is null)
                {
                    throw new MarriageProblemException("next error: on " + i);
                }
                contender = dto.Text;
                if (contender == null)
                {
                    break;
                }
                _princess.ConsiderContender(contender);
                i++;
            } while (!_princess.IsChosenOne);
            
            string pUrl = _url + "/hall/"+ _attempt +"/select?session=" + _session;
            RankDto? rankDto = await (await client.PostAsync(pUrl, new StringContent(""))).Content.ReadFromJsonAsync<RankDto>();
            if (rankDto is null)
            {
                throw new MarriageProblemException("select error");
            }
            score = rankDto.Rank;
        }
        return score > _contenderNumber / 2 ? score : 0;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("score: " + SolveProblem().Result);
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}