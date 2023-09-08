using UnityEngine;

public class ExpShardComponent : MonoBehaviour, ICollectable {
  [SerializeField]
  private int exp = 100;

  public void AddEffectTo(PlayerController player) {
    player.GainExperience(exp);
  }
}