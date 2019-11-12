using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerCenter : MonoBehaviour
{
    public CameraBounds currentMap;

    private void OnTriggerStay2D(Collider2D other) {
        if (other.tag == "MapBounds") {
            if (currentMap != null && other.gameObject.name == currentMap.gameObject.name) return;
            other.GetComponent<CameraBounds>().CinemachineInThisMap.SetActive(true);
            currentMap.CinemachineInThisMap.SetActive(false);
            currentMap = other.GetComponent<CameraBounds>();
        }
    }
}
