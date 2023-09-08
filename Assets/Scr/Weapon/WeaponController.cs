using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class WeaponController : MonoBehaviour {
  private const int damage = 10000;
  private float attackRange = 1f;

  private void OnTriggerEnter(Collider other) {
    if (!other.gameObject.CompareTag(EnemyController.tag)) return;
    var targetAttackable = other.GetComponent<AttackableComponent>();
    if (targetAttackable) targetAttackable.TakeDamage(damage);
  }
}