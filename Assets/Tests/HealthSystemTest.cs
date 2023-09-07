using System.Collections;
using NUnit.Framework;
using UnityEngine.TestTools;

public class HealthSystemTest {
  // A Test behaves as an ordinary method
  [Test]
  public void HealthSystemTestSimplePasses() {
    var health = new Health(100);
    Assert.AreEqual(health._maxHealth, 100);
  }

  // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
  // `yield return null;` to skip a frame.
  [UnityTest]
  public IEnumerator HealthSystemTestWithEnumeratorPasses() {
    // Use the Assert class to test conditions.
    // Use yield to skip a frame.
    yield return null;
  }
}