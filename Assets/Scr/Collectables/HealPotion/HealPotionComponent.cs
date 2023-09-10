using UnityEngine;

public class HealPotionComponent : MonoBehaviour, ICollectable {
  [SerializeField]
  private int heal = 50;

  public void AddEffectTo(PlayerController player) {
    player.Heal(heal);
  }
}