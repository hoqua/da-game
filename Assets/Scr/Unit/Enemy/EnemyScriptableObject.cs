using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "ScriptableObjects/Attackable", order = 1)]

public class EnemyScriptableObject : ScriptableObject
{
  [SerializeField]
  float maxHealth = 100f;
  public float MaxHealth { get => maxHealth; set => maxHealth = value; }
  
  [SerializeField]
  float damage = 10f;
  public float Damage { get => damage; set => damage = value; }
}
