using System.Collections;
using CSharpLabs.Exceptions;
using CSharpLabs.MarriageProblem.Contender;
using CSharpLabs.MarriageProblem.ContenderGenerator;
using CSharpLabs.MarriageProblem.Freind;
using CSharpLabs.MarriageProblem.Hall;
using CSharpLabs.MarriageProblem.Princess;
using MarriageProblemTests.Mocks;

namespace MarriageProblemTests;

public class ContenderGeneratorTests
{
    [Test]
    public void ContenderGeneratorTestUniqueNames()
    {
        var generator = new DefaultContenderGenerator();
        var contenders = generator.GetContenders(100);
        var nameSet = new HashSet<string>();
        var allUnique = true;
        for (var i = 0; i < 100; ++i)
        {
            allUnique &= nameSet.Add(contenders[i].Name);
        }
        Assert.That(allUnique, Is.True);
    }
}

public class FreindTest
{
    [Test]
    public void FreindTestCorrectComparation()
    {
        var freind = new Freind();
        var firstContender = new Contender(1, "firstContender");
        firstContender.IsChecked = true;
        var secondContender = new Contender(2, "secondContender");
        secondContender.IsChecked = true;
        var thirdContender = new Contender(1, "thirdContender");
        thirdContender.IsChecked = true;
        Assert.Multiple(() =>
        {
            Assert.That(freind.GetBestContender(firstContender, secondContender), Is.EqualTo(secondContender));
            Assert.That(freind.GetBestContender(secondContender, firstContender), Is.EqualTo(secondContender));
            Assert.That(freind.GetBestContender(firstContender, thirdContender), Is.EqualTo(firstContender).Or.EqualTo(thirdContender));
            Assert.That(freind.GetBestContender(firstContender, firstContender), Is.EqualTo(firstContender));
            Assert.That(freind.GetBestContender(null, firstContender), Is.EqualTo(firstContender));
            Assert.That(freind.GetBestContender(firstContender, null), Is.EqualTo(firstContender));
            Assert.That(freind.GetBestContender(null, null), Is.EqualTo(null));
        });
    }

    [Test]
    public void FreindTestIsKnownByPrincess()
    {
        var freind = new Freind();
        var firstContender = new Contender(1, "firstContender");
        var secondContender = new Contender(2, "secondContender");
        Assert.Throws<MarriageProblemException>(() =>
        {
            freind.GetBestContender(firstContender, secondContender);
        });
    }
}

public class HallTest
{
    [Test]
    public void HallTestGetContenders()
    {
        var hall = new Hall(100, new DefaultContenderGenerator());
        Assert.Multiple(() =>
        {
            var nameSet = new HashSet<string>();
            var allUnique = true;
            for (var i = 0; i < 100; ++i)
            {
                Assert.That(hall[i], Is.TypeOf(typeof(Contender)));
                allUnique &= nameSet.Add(hall[i].Name);
            }
            Assert.That(allUnique, Is.True);
        });
    }
    
    [Test]
    public void HallTestOutOfRange()
    {
        var hall = new Hall(100, new DefaultContenderGenerator());
        Assert.Throws<MarriageProblemException>(() =>
        {
            var contender = hall[100];
        });
    }
}

public class PrincessTest
{
    private Contender? PrincessLogic(MockContenderGenerator.Index2Contender index2Contender)
    {
        var princess = new Princess(new Freind(), 100, 9);
        var hall = new Hall(100, new MockContenderGenerator(index2Contender));
        var i = 0;
        do
        {
            princess.ConsiderContender(hall[i++]);
        } while (!princess.IsChosenOne && i < 100);
        
        return princess.GetHusband();
    }
    
    [Test]
    public void PrincessTestBusinessLogic()
    {
        Assert.Multiple(() =>
        {
            Assert.That(PrincessLogic((index) => new Contender(index + 1, $"{index + 1}"))?.Score, Is.EqualTo(10));
            Assert.That(PrincessLogic((index) => new Contender(100 - index, $"{index + 1}"))?.Score, Is.EqualTo(1));
            Assert.That(PrincessLogic((index) =>
            {
                if (index < 9)
                {
                    return new Contender(100 - index, $"{index + 1}");
                }
                return new Contender(index - 8, $"{index + 1}");
            })!.Score, Is.EqualTo(91));
        });
        
    }

    [Test]
    public void PrinvessTestOutOfRange()
    {
        Assert.Throws<MarriageProblemException>((() =>
        {
            var princess = new Princess(new Freind(), 100, 9);
            var hall = new Hall(101, new MockContenderGenerator((index) => new Contender(index + 1, $"{index + 1}")));
            var i = 0;
            do
            {
                princess.ConsiderContender(hall[i++]);
            } while (i < 101);
        }));
        
        
    }
}