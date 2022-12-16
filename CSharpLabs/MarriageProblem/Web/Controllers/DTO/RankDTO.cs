namespace CSharpLabs.MarriageProblem.Web.Controllers.DTO;

public class RankDto
{
    public int Rank { get; set; }

    public RankDto(int rank)
    {
        this.Rank = rank;
    }
}