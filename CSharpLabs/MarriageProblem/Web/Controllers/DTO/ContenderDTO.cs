namespace CSharpLabs.MarriageProblem.Web.Controllers;

public class ContenderDTO
{
    public string? Text { get; set; }

    public ContenderDTO(string? text)
    {
        this.Text = text;
    }
}