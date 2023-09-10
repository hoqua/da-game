using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class WeaponController : MonoBehaviour {
  private const int damage = 50;
  private readonly List<AttackableComponent> listOfAttacked = new();
  private ActionComponent actor;
  private float attackRange = 1f;
  private BoxCollider boxCollider;

  private void Awake() {
    actor = transform.root.GetComponent<ActionComponent>();
    boxCollider = GetComponent<BoxCollider>();
    boxCollider.enabled = false;
  }

  private void OnTriggerEnter(Collider other) {
    if (!actor) return;
    if (actor.GetState() != ActionState.Attacking) return;
    if (actor.CompareTag(other.tag)) return;

    var targetAttackable = other.GetComponent<AttackableComponent>();

    if (!targetAttackable) return;
    if (IsAlreadyAttacked(targetAttackable)) return;

    targetAttackable.TakeDamage(damage);
    listOfAttacked.Add(targetAttackable);
  }

  public void StartAttack() {
    boxCollider.enabled = true;
  }

  public void EndAttack() {
    boxCollider.enabled = false;
    listOfAttacked.Clear();
  }

  private bool IsAlreadyAttacked(AttackableComponent attackable) {
    return listOfAttacked.Contains(attackable);
  }
}