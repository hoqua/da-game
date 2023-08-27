using System;
using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour {
  [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
  
  private void Start() {
    
    var heroTransform = GameObject.FindGameObjectWithTag("Player").transform;
    Debug.Log(heroTransform);
    if (!heroTransform) return;

    cinemachineVirtualCamera.LookAt = heroTransform;
    cinemachineVirtualCamera.Follow = heroTransform;
  }
}