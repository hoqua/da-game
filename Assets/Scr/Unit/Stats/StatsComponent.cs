using UnityEngine;

public class StatsComponent : MonoBehaviour {
  [Header("Stats/Ref")]
  [Required]
  [SerializeField]
  private UnitStatsScriptableObject statsObject;

  [Header("Stats/Core")]
  [SerializeField]
  private int attackPower;

  [SerializeField]
  private int defense;

  [Header("Stats/Level")]
  [SerializeField]
  private Level level;

  [SerializeField]
  private Health health;

  private int moveDistance;

  [Header("Stats/Movement")]
  private float moveSpeed;

  private float rotateSpeed;

  private void Start() {
    // Stats/Experience
    level = new Level(statsObject.startLevel);
    health = new Health(statsObject.maxHealth);
    // Stats/Core
    attackPower = statsObject.attackPower;
    defense = statsObject.defense;
    //Stats/Movement
    moveSpeed = statsObject.moveSpeed;
    moveDistance = statsObject.moveDistance;
    rotateSpeed = statsObject.rotateSpeed;
  }

  public void GainExperience(int amount) {
    level.GainExperience(amount);
  }

  public void TakeDamage(int damage) {
    health.TakeDamage(damage);
  }

  public float GetMoveSpeed() {
    return moveSpeed;
  }

  public float GetRotateSpeed() {
    return rotateSpeed;
  }

  public int GetMoveDistance() {
    return moveDistance;
  }

  public void Heal(int healAmount) {
    health.Heal(healAmount);
  }

  public bool IsDead() {
    return health.IsDead();
  }
}