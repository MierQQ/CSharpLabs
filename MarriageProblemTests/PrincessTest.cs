using CSharpLabs.MarriageProblem.Contender;
using CSharpLabs.MarriageProblem.Exceptions;
using CSharpLabs.MarriageProblem.Freind;
using CSharpLabs.MarriageProblem.Princess;
using FluentAssertions;

namespace MarriageProblemTests;

public class PrincessTest
{
    [Test]
    public void PrincessTestBusinessLogic()
    {
        //Arrange
        var princess = new Princess(new Freind(), 100, 9);
        //Act
        for (int i = 0; i < 100; ++i)
        {
            princess.ConsiderContender(new Contender(i + 1, $"{i + 1}"));
        }
        //Assert
        princess.GetHusband().Should()
            .Match(p => princess.IsChosenOne && p != null || !princess.IsChosenOne && p == null);
    }

    [Test]
    public void PrincessTestOutOfRange()
    {
        var princess = new Princess(new Freind(), 100, 9);
        for (int i = 0; i < 100; ++i)
        {
            princess.ConsiderContender(new Contender(i + 1, $"{i + 1}"));
        }

        Action action = () => princess.ConsiderContender(new Contender(101, "101"));

        action.Should().Throw<MarriageProblemException>().WithMessage("Out of range");
    }
}