using UnityEngine;

public class MouseWorld : MonoBehaviour {
  private static MouseWorld instance;
  [SerializeField] private LayerMask mousePlaneMask;
  private Color _originalColor;
  private HeroController _player;
  private MeshRenderer _renderer;

  private void Awake() {
    if (instance != null) {
      Destroy(gameObject);
      return;
    }

    instance = this;
  }

  private void Start() {
    _player = GameObject.FindGameObjectWithTag("Player").GetComponent<HeroController>();
    _renderer = GetComponentInChildren<MeshRenderer>();
    _originalColor = _renderer.material.color;
  }


  private void Update() {
    var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, mousePlaneMask);
    var pointPosition = LevelGrid.Instance.GetPosition(raycastHit.point);
    var playerPosition = LevelGrid.Instance.GetPosition(_player.transform.position);
    var isValid = LevelGrid.Instance.IsValidPosition(pointPosition, playerPosition, _player.GetMoveDistance());
    _renderer.material.color = isValid ? _originalColor : new Color(15, 0, 0, 1f);

    transform.position = raycastHit.point;
  }


  public static Vector3 GetPosition() {
    // TODO: fix duplicated code on unit
    var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

    Physics.Raycast(ray, out var raycastHit, float.MaxValue, instance.mousePlaneMask);
    return raycastHit.point;
  }
}