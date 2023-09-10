using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(StatsComponent))]
public class AttackableComponent : MonoBehaviour {
  [Required]
  public DamagePopUp damagePopupPrefab;

  private Collider _collider;

  private Color _originalColor;
  private SkinnedMeshRenderer _renderer;
  private StatsComponent _stats;


  private void Awake() {
    _renderer = GetComponentInChildren<SkinnedMeshRenderer>();
    _collider = GetComponent<Collider>();
    _stats = GetComponent<StatsComponent>();
  }

  private void Start() {
    _originalColor = _renderer.material.color;
  }

  public Vector3 GetSize() {
    return _renderer.bounds.size;
  }

  public void TakeDamage(int damage) {
    Debug.Log("TOOK DAMAGE");
    _renderer.material.color = new Color(1, 0, 0, .1f);
    // _animator.SetTrigger(Damage);
    _stats.TakeDamage(damage);
    ShowDamageText(damage);
    StartCoroutine(ChangeBackToOriginalMaterial());
    if (_stats.IsDead()) {
      var position = LevelGrid.Instance.GetPosition(transform.position);
      LevelGrid.Instance.RemoveFromCell(position);
      Destroy(gameObject);
    }

    ;
  }

  private void ShowDamageText(int damage) {
    var popup = Instantiate(damagePopupPrefab, transform.position, Quaternion.identity);
    popup.GetComponent<DamagePopUp>()?.SetText(damage.ToString());
  }

  private IEnumerator ChangeBackToOriginalMaterial() {
    // Wait for a certain time before changing the material back
    yield return new WaitForSeconds(.1f); // Change the delay time as needed

    // Revert to the original material
    _renderer.material.color = _originalColor;
  }

  public Vector3 GetClosestPoint(Vector3 position) {
    return _collider.ClosestPoint(position);
  }
}