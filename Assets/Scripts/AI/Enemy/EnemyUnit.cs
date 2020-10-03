using UnityEngine;

public class EnemyUnit : MonoBehaviour {
    [Header("随玩家位置转向")]
    public bool isLookAtPlayer;
    [Header("上下浮动")]
    public bool isFloat;
    public float freq;
    public float amplitude;
    [Header("是否能复活")]
    public bool canBeResurrected;
    [Header("Health")]
    public int MaxHealth;
    public int health { get; set; }
    [HideInInspector]
    public bool IsReviving;

    private Transform playerTransform;
    private Transform enemyTransform;
    private Vector2 oriPos;

    private void Awake()
    {
        health = MaxHealth;
        enemyTransform = transform;
        oriPos = enemyTransform.position;
        if (isLookAtPlayer)
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void Update() 
    {
        if (isLookAtPlayer)
            EnemyLookPlayer();
        if (isFloat)
            EnemyFloat();
    }

    private void EnemyLookPlayer()
    {
        if (playerTransform.position.x >= gameObject.transform.position.x)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
    }
    private void EnemyFloat()
    {
        Vector2 offset = new Vector2(0, amplitude * Mathf.Sin(Time.time * freq));
        enemyTransform.position = (Vector3)(oriPos + offset);
    }

    public virtual void GetHurt(int damage) {
        health -= damage;
        if (health <= 0) {
            gameObject.SetActive(false);
        }
    }
}