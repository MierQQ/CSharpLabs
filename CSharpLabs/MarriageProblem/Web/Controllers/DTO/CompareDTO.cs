using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CSharpLabs.MarriageProblem.Web.Controllers;

public class CompareDTO
{
    public CompareDTO()
    {
    }
    
    public CompareDTO(string name1, string name2)
    {
        this.Name1 = name1;
        this.Name2 = name2;
    }
    
    public string Name1 { get; set; }
    public string Name2 { get; set; }
    
}