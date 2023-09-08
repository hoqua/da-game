public class Health {
  private int _healthPoints;
  private int _maxHealth;

  public Health(int maxHealth) {
    _healthPoints = maxHealth;
    _maxHealth = maxHealth;
  }

  public void SetMaxHealth(int maxHealth) {
    _maxHealth = maxHealth;
  }

  public int GetCurrentHealth() {
    return _healthPoints;
  }

  public int GetMaxHealth() {
    return _maxHealth;
  }

  public void TakeDamage(int damageAmount) {
    _healthPoints -= damageAmount;
    if (IsDead()) _healthPoints = 0;
  }

  public void Heal(int healAmount) {
    if (!IsDead()) _healthPoints += healAmount;
    if (_healthPoints > _maxHealth) _healthPoints = _maxHealth;
  }

  public bool IsDead() {
    return _healthPoints <= 0;
  }
}