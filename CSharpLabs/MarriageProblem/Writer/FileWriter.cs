using CSharpLabs.MarriageProblem.Hall;

namespace CSharpLabs.MarriageProblem.Writer;

public class FileWriter: IWriter
{
    private readonly StreamWriter _streamWriter;
    public FileWriter(StreamWriter streamWriter)
    {
        _streamWriter = streamWriter;
    }
    public void Write(IHall hall, int score)
    {
        for (var i = 0; i < 100; ++i)
        {
            _streamWriter.WriteLine(hall[i].Name + $":{hall[i].Score} {i}");
        }
        _streamWriter.WriteLine("-----------");
        _streamWriter.WriteLine(score);
        _streamWriter.Close();
    }
}