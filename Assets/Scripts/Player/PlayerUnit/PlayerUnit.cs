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
        SetDeathStatus();
        RecoverTimeScale();
    }
    private void SetDeathStatus() {
        if (isDead && canCoroutine) {
            // 不可操作
            _controller.enabled = false;
            // 透明
            Color alpha = new Color(1, 1, 1, 0);
            sprite.color = alpha;
            // 物理清零
            _rigidbody.gravityScale = 0;
            _rigidbody.velocity = Vector2.zero;


            StartCoroutine(Resurrection());
            canCoroutine = false;
        }
    }
    private void RecoverTimeScale() {
        if (isDead) {
            Time.timeScale = 1;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
        }
    }
    private IEnumerator Resurrection() {
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
            transform.SetPositionAndRotation(ResurrectionPoint.transform.position, Quaternion.identity);
        _rigidbody.gravityScale = 4;
        _controller.ResetStatus();
        sprite.color = unAlpha;
        _controller.enabled = true;
        isDead = false;
        canCoroutine = true;
    }
}
