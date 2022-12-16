using CSharpLabs.MarriageProblem.Hall;
using CSharpLabs.MarriageProblem.Princess;
using CSharpLabs.MarriageProblem.Writer;

namespace CSharpLabs.MarriageProblem;

public class MarriageProblem : IHostedService
{
    private readonly int _contenderNumber;
    private readonly IHall _hall;
    private readonly IPrincess _princess;
    private readonly IWriter _writer;
    private const int LevelOfSatisfactionOfChoosingNoOne = 10;

    public MarriageProblem(int contenderNumber, IPrincess princess, IHall hall , IWriter writer)
    {
        _contenderNumber = contenderNumber;
        _hall = hall;
        _writer = writer;
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
        _writer.Write(_hall, SolveProblem());
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        SolveAndPrint();
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}