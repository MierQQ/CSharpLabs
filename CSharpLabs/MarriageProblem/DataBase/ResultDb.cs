namespace CSharpLabs.MarriageProblem.DataBase;

public class ResultDb
{
    public int Id { get; set; }
    public int Result { get; set; }
    public List<AttemptMemberDb> AttemptMembersDbs { get; set; } = new();
}