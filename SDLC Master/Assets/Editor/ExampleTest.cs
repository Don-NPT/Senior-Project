using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class ExampleTest
{
    [Test]
    public void ExampleTestPasses()
    {
        Assert.AreEqual(1, 1);
    }

    [Test]
    public void ExampleTestFails()
    {
        Assert.AreEqual(1, 2);
    }
}
