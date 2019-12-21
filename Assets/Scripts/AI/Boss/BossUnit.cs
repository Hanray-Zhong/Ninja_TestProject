using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossUnit : EnemyUnit
{
    public List<GameObject> status = new List<GameObject>();
    private SpriteRenderer sprite;
    private Vector3 originPos;
    private Color originColor;
    private CircleCollider2D col;

    private void Awake() {
        sprite = gameObject.GetComponent<SpriteRenderer>();
        originPos = transform.position;
        originColor = sprite.color;
        col = gameObject.GetComponent<CircleCollider2D>();
    }

    public void ChangeStatus(int index) {
        foreach (GameObject _status in status) {
            if (status.IndexOf(_status) != index) {
                _status.SetActive(false);
            }
            else {
                _status.SetActive(true);
            }
        }
    }
    public override void GetHurt(int damage) {
        health -= damage;
        if (health <= 0) {
            gameObject.SetActive(false);
        }
        if (gameObject.activeSelf)
            StartCoroutine(Hurt());
    }
    
    IEnumerator Hurt() {
        sprite.color = new Color(1, 1, 1, 0);
        col.enabled = false;
        yield return new WaitForSeconds(3);
        sprite.color = originColor;
        transform.position = originPos;
        col.enabled = true;
    }
}
