using System;
using LacunaGenetics.Jobs;
using NUnit.Framework;

namespace Tests;

public class EncodingTest
{
    [Test]
    public void TestEncoding()
    {
        var strandEncoded = Job.EncodeStrand("CATCGTCAGGAC");

        Assert.AreEqual("TbSh", strandEncoded);
    }

    [Test]
    public void TestDecoding()
    {
        var strand = Job.DecodeStrand("TbSh");

        Assert.AreEqual("CATCGTCAGGAC", strand);
    }
}