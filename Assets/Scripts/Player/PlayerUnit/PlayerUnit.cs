using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanetoTools;

public class PlayerUnit : MonoBehaviour
{
    [Header("ResurrectionPoints")]
    public List<Transform> ResurrectionPoints;
    public Transform ResurrectionPoint;
    [Header("Deadth")]
    public GameObject DeadEffect;
    public CameraFollow cameraFollow;
    public LoadingSceneTransition LoadSceneTransition;

    private bool isDead = false;
    private bool canCoroutine = true;
    private SpriteRenderer sprite;
    private Rigidbody2D _rigidbody;
    private PlayerController _controller;

    public bool IsDead {
        get {return this.isDead;}
        set {this.isDead = value;}
    }

    private void Awake() {
        sprite = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _controller = GetComponent<PlayerController>();
    }

    private void Update() {
        if (isDead && canCoroutine) {
            // 透明
            Color alpha = new Color(1, 1, 1, 0);
            sprite.color = alpha;
            // 物理清零
            _rigidbody.gravityScale = 0;
            _rigidbody.velocity = Vector2.zero;
            // 状态充值
            _controller.ResetStatus();

            cameraFollow.IsFollowing = false;

            StartCoroutine(Resurrection());
            canCoroutine = false;
        }
        if (isDead) {
            Time.timeScale = 1;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
        }
    }
    private IEnumerator Resurrection() {
        if (DeadEffect != null) {
            Instantiate(DeadEffect, transform.position, Quaternion.identity);
        }
        if (ResurrectionPoint != null)
            transform.SetPositionAndRotation(ResurrectionPoint.transform.position, Quaternion.identity);
        _rigidbody.gravityScale = 2;
        // Crountine
        yield return new WaitForSeconds(0.3f);
        if (LoadSceneTransition != null) {
            LoadSceneTransition.CanFade = true;
        }
        yield return new WaitForSeconds(1.2f);
        if (LoadSceneTransition != null) {
            LoadSceneTransition.CanFade = true;
        }
        Color unAlpha = new Color(0, 0, 0, 1);
        sprite.color = unAlpha;
        isDead = false;
        cameraFollow.IsFollowing = true;
        canCoroutine = true;
    }
}
