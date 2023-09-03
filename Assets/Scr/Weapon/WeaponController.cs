using UnityEngine;

// TODO: add required for rigidbody and collider
public class WeaponController : MonoBehaviour {
  private float attackRange = 1f;

  private int damage = 1;

  private void OnTriggerEnter(Collider other) {
    if (other.CompareTag("Attackable")) {
      other.GetComponent<AttackableComponent>().TakeDamage();
    }
  }
}