using CSharpLabs.MarriageProblem.Contender;
using CSharpLabs.MarriageProblem.Exceptions;
using CSharpLabs.MarriageProblem.Freind;
using FluentAssertions;
using Moq;

namespace MarriageProblemTests;

public class FreindTest
{
    [Test]
    public void FreindTestCorrectComparation()
    {
        //Arrange
        var freind = new Freind();
        var firstContenderMock = new Mock<IContender>();
        var secondContenderMock = new Mock<IContender>();
        var thirdContenderMock = new Mock<IContender>();
        firstContenderMock.Setup(p => p.Score).Returns(1);
        secondContenderMock.Setup(p => p.Score).Returns(2);
        thirdContenderMock.Setup(p => p.Score).Returns(1);
        firstContenderMock.Setup(p => p.IsChecked).Returns(true);
        secondContenderMock.Setup(p => p.IsChecked).Returns(true);
        thirdContenderMock.Setup(p => p.IsChecked).Returns(true);
        var firstContender = firstContenderMock.Object;
        var secondContender = secondContenderMock.Object;
        var thirdContender = thirdContenderMock.Object;
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
        var firstContenderMock = new Mock<IContender>();
        var secondContenderMock = new Mock<IContender>();
        firstContenderMock.Setup(p => p.IsChecked).Returns(false);
        secondContenderMock.Setup(p => p.IsChecked).Returns(false);
        var firstContender = firstContenderMock.Object;
        var secondContender = secondContenderMock.Object;
        
        Action action = () => freind.GetBestContender(firstContender, secondContender);
        //Act + Assert
        action.Should().Throw<MarriageProblemException>().WithMessage("Two unknown contenders");
    }
}