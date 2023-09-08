using System.Collections;
using NUnit.Framework;
using UnityEngine.TestTools;

public class HealthSystemTest {
  // A Test behaves as an ordinary method

  [Test]
  public void HealthConstructor_InitializesHealthPointsAndMaxHealth() {
    // Arrange
    var maxHealth = 100;

    // Act
    var health = new Health(maxHealth);

    // Assert
    Assert.AreEqual(maxHealth, health.GetMaxHealth());
  }

  [Test]
  public void SetMaxHealth_SetsMaxHealthCorrectly() {
    // Arrange
    var health = new Health(150);
    var newMaxHealth = 200;

    // Act
    health.SetMaxHealth(newMaxHealth);

    // Assert
    Assert.AreEqual(newMaxHealth, health.GetMaxHealth());
    Assert.AreEqual(150, health.GetCurrentHealth());
  }

  [Test]
  public void TakeDamage_ReducesHealthPoints() {
    // Arrange
    var health = new Health(100);
    var damageAmount = 30;

    // Act
    health.TakeDamage(damageAmount);

    // Assert
    Assert.AreEqual(70, health.GetCurrentHealth());
  }

  [Test]
  public void TakeLethalDamage_ReducesHealthPoints() {
    // Arrange
    var health = new Health(100);
    var damageAmount = 100000;

    // Act
    health.TakeDamage(damageAmount);

    // Assert
    Assert.AreEqual(0, health.GetCurrentHealth());
  }

  [Test]
  public void Heal_IncreasesHealthPoints() {
    // Arrange
    var health = new Health(70);
    var healAmount = 20;

    // Act
    health.TakeDamage(50);
    health.Heal(healAmount);

    // Assert
    Assert.AreEqual(40, health.GetCurrentHealth());
  }

  [Test]
  public void FullHeal_IncreasesHealthPoints() {
    // Arrange
    var health = new Health(70);
    var healAmount = 100000;

    // Act
    health.TakeDamage(50);
    health.Heal(healAmount);

    // Assert
    Assert.AreEqual(70, health.GetCurrentHealth());
  }

  [Test]
  public void IsDead_ReturnsTrueWhenHealthZero() {
    // Arrange
    var health = new Health(0);

    // Act & Assert
    Assert.IsTrue(health.IsDead());
  }

  [Test]
  public void IsDead_ReturnsFalseWhenHealthPositive() {
    // Arrange
    var health = new Health(50);

    // Act & Assert
    Assert.IsFalse(health.IsDead());
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