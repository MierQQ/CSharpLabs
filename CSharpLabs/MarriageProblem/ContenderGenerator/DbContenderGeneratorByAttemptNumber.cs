using CSharpLabs.MarriageProblem.Contender;
using CSharpLabs.MarriageProblem.Contender.ContenderBuilder;
using CSharpLabs.MarriageProblem.DataBase;
using CSharpLabs.MarriageProblem.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace CSharpLabs.MarriageProblem.ContenderGenerator;

public class DbContenderGeneratorByAttemptNumber : IContenderGenerator
{
    private readonly IContenderBuilder _contenderBuilder;
    private readonly ApplicationContext _applicationContext;
    private readonly int _attemptNumber;

    public DbContenderGeneratorByAttemptNumber(IContenderBuilder contenderBuilder, ApplicationContext applicationContext, int attemptNumber)
    {
        _contenderBuilder = contenderBuilder;
        _applicationContext = applicationContext;
        _attemptNumber = attemptNumber;
    }

    public IContender[] GetContenders(int contenderNumber)
    {
        var result = _applicationContext.Results.Include(r => r.AttemptMembersDbs)
            .ThenInclude(am => am.Name)
            .First(r => r.Id == _attemptNumber) ?? throw new MarriageProblemException("No such id attempt");
        var contenders = new IContender[contenderNumber];
        Console.Out.WriteLine($"Must be {result.Result}");
        foreach (var it in result.AttemptMembersDbs)
        {
            contenders[it.Order] = _contenderBuilder.GetContender(it.Score,it.Name.Name);
        }
        return contenders;
    }
}