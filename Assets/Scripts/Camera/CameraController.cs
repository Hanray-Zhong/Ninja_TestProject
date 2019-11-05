using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanetoTools;

public class CameraController : MonoBehaviour
{
    public PlayerCenter playerCenter;

    private CameraFollow cameraFollow;

    private void Awake() {
        cameraFollow = gameObject.GetComponent<CameraFollow>();
    }

    private void Update() {
        if (playerCenter.CurrentMap != cameraFollow.Bounds)
            cameraFollow.Bounds = playerCenter.CurrentMap;
        cameraFollow.UpdateBounds();
    }
}
