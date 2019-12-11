using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class InitCinemachine : MonoBehaviour
{
    private CinemachineVirtualCamera cinemachine;
    private Transform player;
    void Awake() {
        cinemachine = gameObject.GetComponent<CinemachineVirtualCamera>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        cinemachine.Follow = player;
    }

}
