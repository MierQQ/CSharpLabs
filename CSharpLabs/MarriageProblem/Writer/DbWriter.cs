using CSharpLabs.MarriageProblem.DataBase;
using CSharpLabs.MarriageProblem.Hall;

namespace CSharpLabs.MarriageProblem.Writer;

public class DbWriter: IWriter
{
    private readonly ApplicationContext _applicationContext;
    public DbWriter(ApplicationContext applicationContext)
    {
        _applicationContext = applicationContext;
    }
    public void Write(IHall hall, int score)
    {
        var result = new ResultDb() { Result = score };
        _applicationContext.Results.Add(result);
        var names = _applicationContext.Names.ToList();
        for (var i = 0; i < 100; ++i)
        {
            var attemptMember = new AttemptMemberDb() { Name = names.Where(n => n.Name == hall[i].Name).ToArray()[0],
                Order = i,
                Result = result,
                Score = hall[i].Score };
            _applicationContext.AttemptMembers.Add(attemptMember);
        }
        _applicationContext.SaveChanges();
    }
}