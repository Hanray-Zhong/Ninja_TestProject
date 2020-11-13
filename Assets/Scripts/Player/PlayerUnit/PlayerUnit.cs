using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanetoTools;

public class PlayerUnit : MonoBehaviour
{
    private static PlayerUnit _instance;
    public static PlayerUnit Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("Player").GetComponent<PlayerUnit>();
            }
            return _instance;
        }
    }

    [Header("ResurrectionPoints")]
    public List<Transform> ResurrectionPoints;
    public Transform ResurrectionPoint;
    [Header("Deadth")]
    public GameObject DeadEffect;
    public bool IsOnSafeRegion = false;
    [Header("UI")]
    public Animation TransitionBGAnim;

    private bool isDead = false;
    private bool canCoroutine = true;

    private SpriteRenderer _sprite;
    private Transform _transform;
    private Rigidbody2D _rigidbody;
    private PlayerController _controller;
    private Animator _animator;
    private PlayerSoundController playerSoundController;

    public bool IsDead
    {
        get { return this.isDead; }
        set { this.isDead = value; }
    }

    private void Awake()
    {
        _sprite = GetComponent<SpriteRenderer>();

        _transform = transform;
        _rigidbody = GetComponent<Rigidbody2D>();
        _controller = GetComponent<PlayerController>();
        _animator = GetComponent<Animator>();
        playerSoundController = PlayerSoundController.Instance;
    }

    private void Update()
    {
        CheckDeathStatus();
    }

    /// <summary>
    /// 检查是否进入死亡状态
    /// </summary>
    private void CheckDeathStatus()
    {
        if (isDead && canCoroutine)
        {
            // 透明
            _sprite.color = new Color(1, 1, 1, 0);
            // PlayerController 重置
            _controller.FaceRight = true;
            _controller.ResetPlayerControllerStatus();
            // 设置死亡时的物理状态
            _rigidbody.constraints = RigidbodyConstraints2D.FreezePosition;
            _rigidbody.velocity = Vector2.zero;
            // 播放死亡音效、关闭持续性音效
            playerSoundController.Play(PlayerSoundType.dead);
            playerSoundController.StopAll();
            // 重置 timeScale
            Time.timeScale = 1;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
            // 开始复活协程
            StartCoroutine(Resurrection());
            canCoroutine = false;
        }
    }

    private IEnumerator Resurrection()
    {
        if (DeadEffect != null)
        {
            Instantiate(DeadEffect, _transform.position, Quaternion.identity);
        }
        // Crountine
        yield return new WaitForSeconds(0.3f);
        if (TransitionBGAnim != null)
        {
            TransitionBGAnim.Play("TransitionFadeUp");
        }
        yield return new WaitForSeconds(1);
        if (TransitionBGAnim != null)
        {
            TransitionBGAnim.Play("TransitionFadeDown");
        }
        // 移动到复活点
        if (ResurrectionPoint != null)
        {
            _rigidbody.constraints = RigidbodyConstraints2D.None;
            _transform.position = (Vector2)ResurrectionPoint.transform.position + new Vector2(0, 1);
        }
        // 恢复正常物理状态
        _rigidbody.velocity = Vector2.zero;
        _rigidbody.freezeRotation = true;
        _transform.up = Vector2.up;
        _sprite.color = new Color(1, 1, 1, 1);
        // 播放复活动画

        // 消除死亡状态
        yield return new WaitForSeconds(0);
        isDead = false;
        canCoroutine = true;
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "SafeRegion") IsOnSafeRegion = true;
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "SafeRegion") IsOnSafeRegion = false;
    }
}
