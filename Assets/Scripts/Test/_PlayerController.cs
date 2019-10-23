using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _PlayerController : MonoBehaviour
{
    public float Speed;

    private Transform _transform;
    private Rigidbody2D _rigidbody;

    private void Start() {
        _transform = gameObject.GetComponent<Transform>();
        _rigidbody = gameObject.GetComponent<Rigidbody2D>();
    

    }

    private void Update() {
        if (Input.GetKey(KeyCode.W)) {
            // _transform.Translate(Vector2.up * Speed * Time.deltaTime, Space.World);
            _rigidbody.AddForce(Vector2.up * Speed * Time.deltaTime);
        }
        
        // if (Input.GetAxis("Vertical"))
    }
}
