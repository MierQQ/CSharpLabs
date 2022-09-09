using System.Text;
using Microsoft.Extensions.Hosting;

namespace CSharpLabs;

public class MarriageProblem : IHostedService
{
    private readonly int _contenderNumber;
    private readonly IHall _hall;
    private readonly IPrincess _princess;
    private StreamWriter _streamWriter;
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
        int i = 0;
        do
        {
            _princess.ConsiderContender(_hall[i++]);
        } while (!_princess.IsChosenOne() && i < _contenderNumber);

        Contender? husband = _princess.GetHusband();
        if (husband == null)
        {
            return LevelOfSatisfactionOfChoosingNoOne;
        }

        return husband.GetScore() > _contenderNumber / 2 ? _princess.GetHusband()!.GetScore() : 0;
    }

    private void SolveAndPrint()
    {
        IHall hall = _hall;
        for (int i = 0; i < 100; ++i)
        {
            _streamWriter.WriteLine(hall[i]!.GetName() + $":{hall[i]!.GetScore()} {i}");
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