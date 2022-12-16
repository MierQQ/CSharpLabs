namespace CSharpLabs.MarriageProblem.Web.Controllers.DTO;

public class ContenderDto
{
    public string? Text { get; set; }

    public ContenderDto(string? text)
    {
        this.Text = text;
    }
}