using System.Threading.Tasks.Sources;
using CSharpLabs.MarriageProblem.Hall;
using CSharpLabs.MarriageProblem.Princess;
using CSharpLabs.MarriageProblem.Writer;
using Microsoft.Extensions.Hosting;
using System.Net.Http.Headers;
using System.Text.Json;
using CSharpLabs.MarriageProblem.Exceptions;
using CSharpLabs.MarriageProblem.Web.Controllers;

namespace CSharpLabs.MarriageProblem;

public class MarriageProblemWeb : IHostedService
{
    private readonly int _contenderNumber;
    private readonly IPrincessWeb _princess;
    private const int LevelOfSatisfactionOfChoosingNoOne = 10;
    private int _attempt;
    private int _session;
    private string _url;

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
        var score = -1;
        using (HttpClient client = new HttpClient())
        {
            string postResetUrl = _url + "/hall/"+ _attempt +"/reset?session=" + _session;
            if (_attempt == 0)
            {
                postResetUrl = _url + "/hall/reset?session=" + _session;
            }
            AttemptIdDTO? attemptIdDto = await (await client.PostAsync(postResetUrl, new StringContent(""))).Content.ReadFromJsonAsync<AttemptIdDTO>();
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
                ContenderDTO? dto = await (await client.PostAsync(postUrl, new StringContent(""))).Content.ReadFromJsonAsync<ContenderDTO>();
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
            RankDTO? rankDto = await (await client.PostAsync(pUrl, new StringContent(""))).Content.ReadFromJsonAsync<RankDTO>();
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
        System.Console.WriteLine("score: " + SolveProblem().Result);
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}