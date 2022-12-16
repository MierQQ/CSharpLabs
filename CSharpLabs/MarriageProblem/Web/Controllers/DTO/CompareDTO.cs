namespace CSharpLabs.MarriageProblem.Web.Controllers.DTO;

public class CompareDto
{

    
    public CompareDto(string name1, string name2)
    {
        this.Name1 = name1;
        this.Name2 = name2;
    }
    
    public string Name1 { get; set; }
    public string Name2 { get; set; }
    
}