using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int _maxHealth = 100;
    private int _healthPoints;
    private bool _isDead;
    
    private Health(int startingHealth)
    {
        _healthPoints = startingHealth;
    }

    private void TakeDamage(int damageAmount)
    {
        _healthPoints -= damageAmount;
        
        if (_healthPoints <= 0)
            IsDead();
    }

    private void Heal(int healAmount)
    {
        if (_isDead == false)
        {
            _healthPoints += healAmount;
        }
        if (_healthPoints > _maxHealth)
        {
            _healthPoints = _maxHealth;
        }
    }
    private void IsDead()
    {
        _healthPoints = 0;
        _isDead = true;
    }
}
