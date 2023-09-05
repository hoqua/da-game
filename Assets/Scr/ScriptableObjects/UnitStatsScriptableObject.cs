using UnityEngine;

[CreateAssetMenu(fileName = "NewUnitStats", menuName = "ScriptableObjects/Unit Stats")]
public class UnitStatsScriptableObject : ScriptableObject {
  [Header("General")]
  public string unitName = "New Unit";

  public GameObject startingWeapon;

  [SerializeField]
  public int maxHealth = 100;


  public int attackPower = 10;
  public int defense = 5;
  public float moveSpeed = 5.0f;
  public float moveRange = 5.0f;
}