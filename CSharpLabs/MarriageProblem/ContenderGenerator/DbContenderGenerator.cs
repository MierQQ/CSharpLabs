using CSharpLabs.MarriageProblem.Contender;
using CSharpLabs.MarriageProblem.Contender.ContenderBuilder;
using CSharpLabs.MarriageProblem.DataBase;
using CSharpLabs.MarriageProblem.Exceptions;

namespace CSharpLabs.MarriageProblem.ContenderGenerator;

public class DbContenderGenerator: IContenderGenerator
{
    private readonly IContenderBuilder _contenderBuilder;
    private readonly Random _random;
    private readonly ApplicationContext _applicationContext;
    
    private void Shuffle<T>(T[] array)
    {
        for (var i = array.Length - 1; i >= 1; i--)
        {
            var j = _random.Next(i + 1);
            (array[j], array[i]) = (array[i], array[j]);
        }
    }
    
    public DbContenderGenerator(IContenderBuilder contenderBuilder, ApplicationContext applicationContext)
    {
        _applicationContext = applicationContext;
        _contenderBuilder = contenderBuilder;
        _random = new Random();
    }
    public IContender[] GetContenders(int contenderNumber)
    {
        var names = _applicationContext.Names.ToArray();
        var contenders = new Contender.IContender[contenderNumber];
        for (var i = 0; i < contenderNumber; i++)
        {
            contenders[i] = _contenderBuilder.GetContender(i + 1, names[i].Name ?? throw new MarriageProblemException("Null in name field"));
        }
        Shuffle(contenders);
        return contenders;
    }
}