using CSharpLabs.MarriageProblem.Hall;

namespace CSharpLabs.MarriageProblem.Writer;

public interface IWriter
{
    public void Write(IHall hall, int score);
}