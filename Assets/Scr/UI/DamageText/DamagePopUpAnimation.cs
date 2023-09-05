using TMPro;
using UnityEngine;

public class DamagePopUpAnimation : MonoBehaviour {
  public AnimationCurve opacityCurve;
  public AnimationCurve scaleCurve;
  public AnimationCurve heightCurve;
  private Vector3 origin;
  private float time;

  private TextMeshProUGUI tmp;

  private void Awake() {
    tmp = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    origin = transform.position;
  }

  private void Update() {
    tmp.color = new Color(1, 1, 1, opacityCurve.Evaluate(time));
    transform.localScale = Vector3.one * scaleCurve.Evaluate(time);
    transform.position = origin + new Vector3(0, 1 + heightCurve.Evaluate(time), 0);
    time += Time.deltaTime;
  }
}