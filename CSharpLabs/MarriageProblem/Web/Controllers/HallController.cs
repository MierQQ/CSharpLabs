using System.Text.Json;
using CSharpLabs.MarriageProblem.Contender;
using CSharpLabs.MarriageProblem.Contender.ContenderBuilder;
using CSharpLabs.MarriageProblem.ContenderGenerator;
using CSharpLabs.MarriageProblem.DataBase;
using CSharpLabs.MarriageProblem.Hall;
using CSharpLabs.MarriageProblem.Writer;
using Microsoft.AspNetCore.Mvc;

namespace CSharpLabs.MarriageProblem.Web.Controllers;

[Route("hall")]
public class HallController : Controller
{
    public static HallController Instance;
    public Dictionary<int, IHall> _hall;
    private Dictionary<int, int> _state;
    private int _contendrNumber = 100;
    private ApplicationContext _applicationContext;

    public HallController()
    {
        _applicationContext = new ApplicationContext();
        Instance = this;
        _hall = new Dictionary<int, IHall>();
        _state = new Dictionary<int, int>();
    }
    
    [Produces("application/json")]
    [HttpPost("reset")]
    public AttemptIdDTO Reset(int session)
    {
        int id = _applicationContext.Results.Max( r => r.Id) + 1;
        _hall.Add(id, new Hall.Hall(_contendrNumber, new DbContenderGenerator(new ContenderBuilder(), _applicationContext)));
        _state.Add(id, 0);
        System.Console.WriteLine(JsonSerializer.Serialize(new AttemptIdDTO(id)));
        return new AttemptIdDTO(id);
    }
    
    [Produces("application/json")]
    [HttpPost("{attempt}/reset")]
    public AttemptIdDTO ResetById(int attempt, int session)
    {
        if (_hall.ContainsKey(attempt))
        {
            _state[attempt] = 0;
            return new AttemptIdDTO(attempt);
        }
        _state.Add(attempt, 0);
        _hall.Add(attempt, new Hall.Hall(_contendrNumber, new DbContenderGeneratorByAttemptNumber(new ContenderBuilder(), _applicationContext, attempt)));
        return new AttemptIdDTO(attempt);
    }

    [Produces("application/json")]
    [HttpPost("{attempt}/next")]
    public ContenderDTO Next(int attempt, int session)
    {
        _state[attempt] += 1;
        if (_state[attempt] >= _contendrNumber)
        {
            _state[attempt] = _contendrNumber;
            return new ContenderDTO(null);
        }
        IContender contender = _hall[attempt][_state[attempt]];
        contender.IsChecked = true;
        return new ContenderDTO(contender.Name);
    }
    
    [Produces("application/json")]
    [HttpPost("{attempt}/select")]
    public RankDTO Select(int attempt, int session)
    {
        IWriter writer = new DbWriter(_applicationContext);
        if (_state[attempt] == _contendrNumber)
        {
            writer.Write(_hall[attempt], 10);
            return new RankDTO(10);
        }
        IContender contender = _hall[attempt][_state[attempt]];
        writer.Write(_hall[attempt], contender.Score);
        return new RankDTO(_hall[attempt][_state[attempt]].Score);
    }
}