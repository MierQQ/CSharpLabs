namespace CSharpLabs.MarriageProblem.Princess;

public interface IPrincessWeb
{
    public bool IsChosenOne { get; }
    public void ConsiderContender(string contender);
    public string? GetHusband();

    public int Attempt
    {
        set;
    }

    public int Session
    {
        set;
    }


    public HttpClient Client
    {
        set;
    }
}