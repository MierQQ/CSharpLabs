using CSharpLabs.MarriageProblem.Contender;
using CSharpLabs.MarriageProblem.Exceptions;
using CSharpLabs.MarriageProblem.Freind;
using FluentAssertions;

namespace MarriageProblemTests;

public class FreindTest
{
    [Test]
    public void FreindTestCorrectComparation()
    {
        //Arrange
        var freind = new Freind();
        var firstContender = new Contender(1, "firstContender")
        {
            IsChecked = true
        };
        var secondContender = new Contender(2, "secondContender")
        {
            IsChecked = true
        };
        var thirdContender = new Contender(1, "thirdContender")
        {
            IsChecked = true
        };
        //Act + Assert
        Assert.Multiple(() =>
        {
            freind.GetBestContender(firstContender, secondContender).Should().Be(secondContender);
            freind.GetBestContender(secondContender, firstContender).Should().Be(secondContender);
            freind.GetBestContender(firstContender, thirdContender).Should().Match(p => p == firstContender || p == thirdContender);
            freind.GetBestContender(firstContender, firstContender).Should().Be(firstContender);
            freind.GetBestContender(null, firstContender).Should().Be(firstContender);
            freind.GetBestContender(firstContender, null).Should().Be(firstContender);
            freind.GetBestContender(null, null).Should().BeNull();
        });
    }

    [Test]
    public void FreindTestIsKnownByPrincess()
    {
        //Arrange
        var freind = new Freind();
        var firstContender = new Contender(1, "firstContender");
        var secondContender = new Contender(2, "secondContender");
        Action action = () => freind.GetBestContender(firstContender, secondContender);
        //Act + Assert
        action.Should().Throw<MarriageProblemException>().WithMessage("Comparation of two unknown contenders");
    }
}