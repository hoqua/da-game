using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: add required for rigidbody and collider
public class WeaponController : MonoBehaviour
{

  private int damage = 1;
  private float attackRange = 1f;
  
  private void OnTriggerEnter(Collider other)
  {
    Debug.Log("OnTriggerEnter");
    if (other.CompareTag("Attackable"))
    {
      other.GetComponent<AttackableComponent>().TakeDamage();
    }
  }
}
