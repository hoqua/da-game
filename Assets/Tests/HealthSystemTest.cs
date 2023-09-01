using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class HealthSystemTest
{
    // A Test behaves as an ordinary method
    [Test]
    public void HealthSystemTestSimplePasses()
    {
        Assert.AreEqual(1, 1);
    }
    
    [Test]
    public void HealthSystemTestSimpleFails()
    {
      Assert.AreEqual(1, 2);
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator HealthSystemTestWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}
