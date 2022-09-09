using System.Text;

namespace CSharpLabs;

class Program
{
    public static void Main(string[] args)
    {
        MarriageProblem marriageProblem = new MarriageProblem(int.Parse(args[0]), int.Parse(args[1])); //according statistics best threshold equals 9
        Hall hall = marriageProblem.GetHall();
        StreamWriter streamWriter = new StreamWriter("out.txt", false, Encoding.UTF8);
        for (int i = 0; i < 100; ++i)
        {
            streamWriter.WriteLine(hall.Get(i)!.GetName() + $":{hall.Get(i)!.GetScore()} {i}");
        }
        streamWriter.WriteLine("-----------");
        streamWriter.WriteLine(marriageProblem.SolveProblem());
        streamWriter.Close();
    }
}