using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookObj : MonoBehaviour
{
    public GameObject Target;

    private Rigidbody2D target_rigidbidy;
    private float normalVelocity;
    private float tangentVelocity;
    // private float 

    private void Awake() {
        target_rigidbidy = Target.GetComponent<Rigidbody2D>();
    }

    private void Update() {
        
    }
    
}
