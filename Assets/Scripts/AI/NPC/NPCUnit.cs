using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity;

public class NPCUnit : MonoBehaviour
{
    public float Max_Velocity;
    public float interactRadius;
    [Header("Player Interact")]
    public PlayerController playerController;
    public bool canBeCarried = false;
    public bool lookAtPlayer;
    
    private Rigidbody2D _rigidbody;
    private Transform _transform;
    private Transform playerTransform;

    private void Start() {
        _rigidbody = gameObject.GetComponent<Rigidbody2D>();
        _transform = gameObject.transform;
        playerTransform = playerController.gameObject.transform;
    }
    private void Update() {
        if (_rigidbody.velocity.magnitude > Max_Velocity) {
            _rigidbody.velocity = _rigidbody.velocity.normalized * Max_Velocity;
        }
        if (canBeCarried) {
            if (Mathf.Abs(playerTransform.position.x - _transform.position.x) < interactRadius) {
                playerController.allowThrowCube = false;
                playerController.canCarryNPC = true;
                playerController.carriedNPC = gameObject;
            }
            else {
                playerController.allowThrowCube = true;
                playerController.canCarryNPC = false;
            }
        }
        if (lookAtPlayer) {
            if (playerTransform.position.x >= gameObject.transform.position.x) {
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }
            else {
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }
        }
    }
}
