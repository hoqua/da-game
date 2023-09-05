using UnityEngine;

[CreateAssetMenu(fileName = "NewUnitStats", menuName = "ScriptableObjects/Unit Stats")]
public class UnitStatsScriptableObject : ScriptableObject {
  [SerializeField] public GameObject startingWeapon { get; }

  [SerializeField] public int maxHealth { get; } = 100;
  [SerializeField] public int maxMana { get; } = 100;

  [SerializeField] public int attack { get; }
  [SerializeField] public int defense { get; }

  [SerializeField] public int moveSpeed { get; } = 3;
  [SerializeField] public int moveRange { get; } = 2;

  [SerializeField] public int attackRange { get; }
}