using CSharpLabs.MarriageProblem.Contender;
using CSharpLabs.MarriageProblem.Exceptions;
using CSharpLabs.MarriageProblem.Freind;
using CSharpLabs.MarriageProblem.Princess;
using FluentAssertions;
using Moq;

namespace MarriageProblemTests;

public class PrincessTest
{
    [Test]
    public void PrincessTestBusinessLogic()
    {
        //Arrange
        var contenderMock = new Mock<IContender>();
        var freindMock = new Mock<IFreind>();
        freindMock.Setup(p => p.GetBestContender(It.IsAny<IContender?>(), It.IsAny<IContender?>())).Returns(contenderMock.Object);
        var princess = new Princess(freindMock.Object, 100, 9);
        //Act
        for (var i = 0; i < 100; ++i)
        {
            princess.ConsiderContender(contenderMock.Object);
        }
        //Assert
        
        princess.GetHusband().Should()
            .Match(p => princess.IsChosenOne && p != null || !princess.IsChosenOne && p == null);
    }

    [Test]
    public void PrincessTestOutOfRange()
    {
        //Arrange
        var contenderMock = new Mock<IContender>();
        var freindMock = new Mock<IFreind>();
        freindMock.Setup(p => p.GetBestContender(It.IsAny<IContender?>(), It.IsAny<IContender?>())).Returns(contenderMock.Object);
        var princess = new Princess(freindMock.Object, 100, 9);
        for (var i = 0; i < 100; ++i)
        {
            princess.ConsiderContender(contenderMock.Object);
        }
        var action = () => princess.ConsiderContender(contenderMock.Object);
        //Act + Assert
        action.Should().Throw<MarriageProblemException>().WithMessage("Out of range");
    }
}