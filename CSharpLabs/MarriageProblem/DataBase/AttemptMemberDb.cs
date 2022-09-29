namespace CSharpLabs.MarriageProblem.DataBase;

public class AttemptMemberDb
{
    public int Id { get; set; }

    public int NameDbId { get; set; }
    public NameDb Name { get; set; }
    public int Order { get; set; }
    public int Score { get; set; }
    public int ResultDbId { get; set; }
    public ResultDb Result { get; set; }
}