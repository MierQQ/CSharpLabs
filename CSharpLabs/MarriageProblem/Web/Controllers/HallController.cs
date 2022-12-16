using CSharpLabs.MarriageProblem.Contender;
using CSharpLabs.MarriageProblem.Contender.ContenderBuilder;
using CSharpLabs.MarriageProblem.ContenderGenerator;
using CSharpLabs.MarriageProblem.DataBase;
using CSharpLabs.MarriageProblem.Hall;
using CSharpLabs.MarriageProblem.Web.Controllers.DTO;
using CSharpLabs.MarriageProblem.Writer;
using Microsoft.AspNetCore.Mvc;

namespace CSharpLabs.MarriageProblem.Web.Controllers;

[Route("hall")]
public class HallController : Controller
{
    public static HallController? Instance;
    public readonly Dictionary<int, IHall> Hall;
    private readonly Dictionary<int, int> _state;
    private readonly int _contendrNumber = 100;
    private readonly ApplicationContext _applicationContext;

    public HallController()
    {
        _applicationContext = new ApplicationContext();
        Instance = this;
        Hall = new Dictionary<int, IHall>();
        _state = new Dictionary<int, int>();
    }
    
    [Produces("application/json")]
    [HttpPost("reset")]
    public AttemptIdDto Reset(int session)
    {
        int id = _applicationContext.Results.Max( r => r.Id) + 1;
        Hall.Add(id, new Hall.Hall(_contendrNumber, new DbContenderGenerator(new ContenderBuilder(), _applicationContext)));
        _state.Add(id, 0);
        return new AttemptIdDto(id);
    }
    
    [Produces("application/json")]
    [HttpPost("{attempt}/reset")]
    public AttemptIdDto ResetById(int attempt, int session)
    {
        if (Hall.ContainsKey(attempt))
        {
            _state[attempt] = 0;
            return new AttemptIdDto(attempt);
        }
        _state.Add(attempt, 0);
        Hall.Add(attempt, new Hall.Hall(_contendrNumber, new DbContenderGeneratorByAttemptNumber(new ContenderBuilder(), _applicationContext, attempt)));
        return new AttemptIdDto(attempt);
    }

    [Produces("application/json")]
    [HttpPost("{attempt}/next")]
    public ContenderDto Next(int attempt, int session)
    {
        _state[attempt] += 1;
        if (_state[attempt] >= _contendrNumber)
        {
            _state[attempt] = _contendrNumber;
            return new ContenderDto(null);
        }
        IContender contender = Hall[attempt][_state[attempt]];
        contender.IsChecked = true;
        return new ContenderDto(contender.Name);
    }
    
    [Produces("application/json")]
    [HttpPost("{attempt}/select")]
    public RankDto Select(int attempt, int session)
    {
        IWriter writer = new DbWriter(_applicationContext);
        if (_state[attempt] == _contendrNumber)
        {
            writer.Write(Hall[attempt], 10);
            return new RankDto(10);
        }
        IContender contender = Hall[attempt][_state[attempt]];
        writer.Write(Hall[attempt], contender.Score);
        return new RankDto(Hall[attempt][_state[attempt]].Score);
    }
}