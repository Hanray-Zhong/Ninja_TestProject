using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResurrectionPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            PlayerUnit u = other.GetComponent<PlayerUnit>();
            u.ResurrectionPoint = transform;
            if (!u.ResurrectionPoints.Contains(transform))
                u.ResurrectionPoints.Add(transform);
        }
    }
}
