using CSharpLabs.MarriageProblem.Contender;
using CSharpLabs.MarriageProblem.ContenderGenerator;
using CSharpLabs.MarriageProblem.Exceptions;
using CSharpLabs.MarriageProblem.Hall;
using FluentAssertions;
using Moq;

namespace MarriageProblemTests;

public class HallTest
{
    [Test]
    public void HallTestGetContenders()
    {
        //Arrange
        var contenderMock = new Mock<IContender>();
        var contender = contenderMock.Object;
        var contenders = new IContender[100];
        for (var i = 0; i < 100; ++i)
        {
            contenders[i] = contender;
        }
        var contenderGenerator = new Mock<IContenderGenerator>();
        contenderGenerator.Setup(p => p.GetContenders(100)).Returns(contenders);
        var hall = new Hall(100, contenderGenerator.Object);
        //Act + Assert
        Assert.Multiple(() =>
        {
            for (var i = 0; i < 100; ++i)
            {
                hall[i].Should().Be(contender);
            }
        });
    }
    
    [Test]
    public void HallTestOutOfRange()
    {
        //Arrange
        var contenderMock = new Mock<IContender>();
        var contender = contenderMock.Object;
        var contenders = new IContender[100];
        for (var i = 0; i < 100; ++i)
        {
            contenders[i] = contender;
        }
        var contenderGenerator = new Mock<IContenderGenerator>();
        contenderGenerator.Setup(p => p.GetContenders(100)).Returns(contenders);
        var hall = new Hall(100, contenderGenerator.Object);
        var action = () =>
        {
            var tmp = hall[100];
        };
        //Act + assert
        action.Should().Throw<MarriageProblemException>().WithMessage("Out of range");
    }
}