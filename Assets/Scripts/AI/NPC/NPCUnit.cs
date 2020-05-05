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
    public PlayerUnit playerUnit;
    public bool canBeCarried = false;
    public bool lookAtPlayer;
    
    private Rigidbody2D _rigidbody;
    private Transform _transform;
    private Transform playerTransform;

    public bool IsDead { get; set; }

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
            if (Mathf.Abs(playerTransform.position.x - _transform.position.x) < interactRadius && Mathf.Abs(playerTransform.position.y - _transform.position.y) < interactRadius) {
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

    /*private IEnumerator Resurrection() {
        if (DeadEffect != null) {
            Instantiate(DeadEffect, transform.position, Quaternion.identity);
        }
        // Crountine
        yield return new WaitForSeconds(0.3f);
        if (LoadSceneTransition != null) {
            LoadSceneTransition.CanFade = true;
        }
        yield return new WaitForSeconds(1.5f);
        if (LoadSceneTransition != null) {
            LoadSceneTransition.CanFade = true;
        }
        Color unAlpha = new Color(1, 1, 1, 1);
        // 状态重置
        if (ResurrectionPoint != null)
            transform.position = (Vector2)ResurrectionPoint.transform.position + new Vector2(0, 1);
        _rigidbody.gravityScale = oriGravityScale;
        _rigidbody.velocity = Vector2.zero;
        _controller.ResetStatus();
        sprite.color = unAlpha;
        _controller.enabled = true;
        isDead = false;
        canCoroutine = true;
    }*/
}
