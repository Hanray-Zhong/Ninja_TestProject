using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public bool battleStart = false;

    private void Update() {
        if (!battleStart) return;
    }
}
