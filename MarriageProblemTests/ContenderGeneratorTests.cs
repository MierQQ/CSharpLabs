using CSharpLabs.MarriageProblem.ContenderGenerator;
using FluentAssertions;

namespace MarriageProblemTests;

public class ContenderGeneratorTests
{
    [Test]
    public void ContenderGeneratorTestUniqueNames()
    {
        //Arrange
        var generator = new DefaultContenderGenerator();
        //Act
        var contenders = generator.GetContenders(100);
        //Assert
        var nameSet = new string[100];
        for (var i = 0; i < 100; ++i)
        {
            nameSet[i] = contenders[i].Name;
        }
        nameSet.ToHashSet().Should().HaveCount(100, "because there are 100 unique names")
            .And.NotContainNulls("because names cant be null");
    }
}