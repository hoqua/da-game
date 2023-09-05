using UnityEngine;

// TODO: add required for rigidbody and collider
public class WeaponController : MonoBehaviour {
  private const int damage = 10000;
  private readonly string attackableTag = "Attackable";
  private float attackRange = 1f;

  private void Awake() {
    gameObject.tag = attackableTag;
  }

  private void OnTriggerEnter(Collider other) {
    if (other.CompareTag(attackableTag)) other.GetComponent<AttackableComponent>()?.TakeDamage(damage);
  }
}