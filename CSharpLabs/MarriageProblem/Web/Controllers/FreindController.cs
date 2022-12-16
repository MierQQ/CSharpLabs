using CSharpLabs.MarriageProblem.Contender;
using CSharpLabs.MarriageProblem.Exceptions;
using CSharpLabs.MarriageProblem.Freind;
using CSharpLabs.MarriageProblem.Hall;
using Microsoft.AspNetCore.Mvc;

namespace CSharpLabs.MarriageProblem.Web.Controllers;

[Route("freind")]
public class FreindController : Controller
{
    private IFreind _freind;

    public FreindController()
    {
        _freind = new Freind.Freind();
    }
    
    [HttpPost("{attempt}/compare")]
    public ContenderDTO Compare(int attempt, int session, string name1, string name2)
    {
        IHall hall = HallController.Instance._hall[attempt] ?? throw new MarriageProblemException("No such attempt");
        Dictionary<string, IContender> hallDict = new Dictionary<string, IContender>();
        for (int i = 0; i < 100; ++i)
        {
            hallDict.Add(hall[i].Name, hall[i]);
        }

        if (!hallDict.ContainsKey(name1))
        {
            throw new MarriageProblemException("name1: no such contender");
        }
        
        if (!hallDict.ContainsKey(name2))
        {
            throw new MarriageProblemException("name2: no such contender");
        }

        IContender contender1 = hallDict[name1];
        IContender contender2 = hallDict[name2];

        IContender bestContender = _freind.GetBestContender(contender1, contender2) ?? throw new MarriageProblemException("Freind returned null");

        return new ContenderDTO(bestContender.Name);
    }
}