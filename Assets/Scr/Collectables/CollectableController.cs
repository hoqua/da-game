using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class CollectableController : MonoBehaviour {
  public readonly string tag = "Collectable";
  private ICollectable _collectable;

  private void Awake() {
    gameObject.tag = tag;
    _collectable = GetComponent<ICollectable>();
    if (_collectable is null) Debug.Log("Collectable is configured incorrectly.");
  }

  private void OnTriggerEnter(Collider other) {
    if (!other.CompareTag(PlayerController.tag)) return;

    var player = other.GetComponent<PlayerController>();
    _collectable.AddEffectTo(player);

    Destroy(gameObject);
  }
}

public interface ICollectable {
  void AddEffectTo(PlayerController player);
}