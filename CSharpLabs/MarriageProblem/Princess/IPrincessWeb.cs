namespace CSharpLabs.MarriageProblem.Princess;

public interface IPrincessWeb
{
    public bool IsChosenOne { get; }
    public void ConsiderContender(string contender);
    public string? GetHusband();

    public int Attempt { get; set; }

    public int Session { get; set; }


    public HttpClient Client { get; set; }
}