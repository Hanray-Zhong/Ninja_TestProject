using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnit : MonoBehaviour
{
    [Header("ResurrectionPoints")]
    public List<Transform> ResurrectionPoints;
    public Transform ResurrectionPoint;
    [Header("Deadth")]
    public GameObject DeadEffect;
    public LoadingSceneTransition LoadSceneTransition;

    private bool isDead = false;
    private SpriteRenderer sprite;
    private Rigidbody2D _rigidbody;

    public bool IsDead {
        get {return this.isDead;}
        set {this.isDead = value;}
    }

    private void Awake() {
        sprite = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        if (isDead) {
            // 透明
            Color alpha = new Color(1, 1, 1, 0);
            sprite.color = alpha;
            // 物理清零
            _rigidbody.gravityScale = 0;
            _rigidbody.velocity = Vector2.zero;

            if (DeadEffect != null) {
                Instantiate(DeadEffect, transform.position, Quaternion.identity);
            }
            StartCoroutine(Resurrection());
            isDead = false;
        }
        
    }
    private IEnumerator Resurrection() {
        yield return new WaitForSeconds(0.3f);
        if (LoadSceneTransition != null) {
            LoadSceneTransition.CanFade = true;
        }
        yield return new WaitForSeconds(1f);
        if (LoadSceneTransition != null) {
            LoadSceneTransition.CanFade = true;
        }
        if (ResurrectionPoint != null)
            transform.SetPositionAndRotation(ResurrectionPoint.transform.position, Quaternion.identity);
        _rigidbody.gravityScale = 2;
        Color unAlpha = new Color(0, 0, 0, 1);
        sprite.color = unAlpha;
    }
}
