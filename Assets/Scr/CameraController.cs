using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour {
  [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;

  private void Start() {
    var heroTransform = GameObject.FindGameObjectWithTag("Player").transform;
    if (!heroTransform) return;

    cinemachineVirtualCamera.LookAt = heroTransform;
    cinemachineVirtualCamera.Follow = heroTransform;
  }
}