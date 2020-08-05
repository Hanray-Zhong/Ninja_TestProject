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
    public bool IsOnSafeRegion = false;
    [Header("UI")]
    public LoadingSceneTransition LoadSceneTransition;

    private bool isDead = false;
    private bool canCoroutine = true;
    private SpriteRenderer sprite;
    private Transform _transform;
    private Rigidbody2D _rigidbody;
    private PlayerController _controller;
    private Animator _animator;
    private float oriGravityScale;

    public bool IsDead {
        get {return this.isDead;}
        set {this.isDead = value;}
    }

    private void Awake() {
        sprite = GetComponent<SpriteRenderer>();
        _transform = transform;
        _rigidbody = GetComponent<Rigidbody2D>();
        _controller = GetComponent<PlayerController>();
        oriGravityScale = _rigidbody.gravityScale;
        _animator = GetComponent<Animator>();
    }

    private void Update() {
        SetDeathStatus();
        RecoverTimeScale();
    }
    private void SetDeathStatus() {
        if (isDead && canCoroutine) {
            // 不可操作
            _controller.Arrow.SetActive(false);
            // 断开绳索连接
            _controller.Rope.gameObject.SetActive(false);
            if (_controller.NearestHook != null) {
                _controller.NearestHook.GetComponent<HingeJoint2D>().connectedBody = null;
                _controller.NearestHook = null;
            }
            // 透明
            Color alpha = new Color(1, 1, 1, 0);
            sprite.color = alpha;
            // 物理清零
            _rigidbody.gravityScale = 0;
            _rigidbody.constraints = RigidbodyConstraints2D.FreezePosition;
            _rigidbody.velocity = Vector2.zero;
            // 开始复活协程
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
            Instantiate(DeadEffect, _transform.position, Quaternion.identity);
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
        // 复活点
        if (ResurrectionPoint != null)
        {
            _rigidbody.constraints = RigidbodyConstraints2D.None;
            _transform.position = (Vector2)ResurrectionPoint.transform.position + new Vector2(0, 1);
        }
        // 物理重置
        _rigidbody.gravityScale = oriGravityScale;
        _rigidbody.velocity = Vector2.zero;
        // 状态重置
        _rigidbody.freezeRotation = true;
        _transform.up = Vector2.up;
        _controller.ResetStatus();
        sprite.color = unAlpha;
        // 播放复活动画
        
        // 消除死亡状态
        yield return new WaitForSeconds(0);
        isDead = false;
        canCoroutine = true;
    }
    private void OnTriggerStay2D(Collider2D other) {
        if (other.tag == "SafeRegion") IsOnSafeRegion = true;
    }
    private void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "SafeRegion") IsOnSafeRegion = false;
    }
}
