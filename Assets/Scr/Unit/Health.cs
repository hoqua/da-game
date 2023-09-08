using UnityEngine;

public class Health : MonoBehaviour {
  public int _maxHealth;
  public int _healthPoints;

  public Health(int maxHealth) {
    _healthPoints = maxHealth;
    _maxHealth = maxHealth;
  } 

  public void SetMaxHealth(int maxHealth)
  {
    _maxHealth = maxHealth;
  }

  public void TakeDamage(int damageAmount) {
    _healthPoints -= damageAmount;
    if(IsDead()) _healthPoints = 0;
  }

  public void Heal(int healAmount) {
    if (!IsDead()) _healthPoints += healAmount;
    if (_healthPoints > _maxHealth) _healthPoints = _maxHealth;
  }

  public bool IsDead() {
    if (_healthPoints <= 0)  return true; 
    return false;
  }
}