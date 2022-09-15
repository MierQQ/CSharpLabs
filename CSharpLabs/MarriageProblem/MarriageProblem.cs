using CSharpLabs.MarriageProblem.Hall;
using CSharpLabs.MarriageProblem.Princess;
using Microsoft.Extensions.Hosting;

namespace CSharpLabs.MarriageProblem;

public class MarriageProblem : IHostedService
{
    private readonly int _contenderNumber;
    private readonly IHall _hall;
    private readonly IPrincess _princess;
    private readonly StreamWriter _streamWriter;
    private const int LevelOfSatisfactionOfChoosingNoOne = 10;

    public MarriageProblem(int contenderNumber, IPrincess princess, IHall hall , StreamWriter streamWriter)
    {
        _contenderNumber = contenderNumber;
        _hall = hall;
        _streamWriter = streamWriter;
        _princess = princess;
    }

    private int SolveProblem()
    {
        var i = 0;
        do
        {
            _princess.ConsiderContender(_hall[i++]);
        } while (!_princess.IsChosenOne && i < _contenderNumber);

        var husband = _princess.GetHusband();
        if (husband == null)
        {
            return LevelOfSatisfactionOfChoosingNoOne;
        }

        return husband.Score > _contenderNumber / 2 ? _princess.GetHusband()!.Score : 0;
    }

    private void SolveAndPrint()
    {
        var hall = _hall;
        for (var i = 0; i < 100; ++i)
        {
            _streamWriter.WriteLine(hall[i].Name + $":{hall[i].Score} {i}");
        }
        _streamWriter.WriteLine("-----------");
        _streamWriter.WriteLine(SolveProblem());
        _streamWriter.Close();
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        SolveAndPrint();
        _streamWriter.Close();
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}