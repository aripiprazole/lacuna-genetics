using LacunaGenetics.Jobs;
using NUnit.Framework;

namespace Tests;

public class GeneTest
{
    [Test]
    public void TestNotContains50Percent()
    {
        Assert.IsFalse(Job.CheckGene("string", "ABCDEFJHIJKL"));
    }
    
    [Test]
    public void TestContains50Percent()
    {
        Assert.IsTrue(Job.CheckGene("ABCDEFJHI", "ABCDEFJHIJKL"));
    }
    
    [Test]
    public void TestContains100Percent()
    {
        Assert.IsTrue(Job.CheckGene("ABCDEFJHIJKL", "ABCDEFJHIJKL"));
    }
}