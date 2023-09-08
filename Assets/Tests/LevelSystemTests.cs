using NUnit.Framework;

public class LevelTests {
  private Level testLevel;

  [SetUp]
  public void SetUp() {
    testLevel = new Level();
  }

  [TearDown]
  public void TearDown() {
    testLevel = null;
  }

  [Test]
  public void LevelStartsAtDefaultValue() {
    Assert.AreEqual(1, testLevel.GetLevel()); // Assuming you have a GetCurrentLevel() method
  }

  [Test]
  public void GainExperience_IncreasesExperience() {
    var initialExperience = testLevel.GetExperience(); // Assuming you have a GetCurrentExperience() method
    testLevel.GainExperience(50);
    Assert.AreEqual(initialExperience + 50, testLevel.GetExperience());
  }

  [Test]
  public void GainExperience_LevelUpsWhenExperienceCapIsReached() {
    var initialLevel = testLevel.GetLevel();
    testLevel.GainExperience(100);
    Assert.AreEqual(initialLevel + 1, testLevel.GetLevel());
  }
}