using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject[] Enemies;
    public Vector2[] EnemiesPos;
    public PlayerUnit playerUnit;
    private bool canInvoke = true;

    private void Awake() {
        for (int i = 0; i < Enemies.Length; i++) {
            EnemiesPos[i] = Enemies[i].transform.position;
        }
    }
    private void Update() {
        if (!playerUnit.IsDead) canInvoke = true;
        if (playerUnit != null && playerUnit.IsDead && canInvoke) {
            Invoke("ReviveEnemies", 1.5f);
            canInvoke = false;
        }

        foreach (var enemy in Enemies) {
            EnemyUnit eu = enemy.GetComponent<EnemyUnit>();
            if (!enemy.activeSelf && !eu.IsReviving) {
                eu.IsReviving = true;
                StartCoroutine(ReviveEnemy(enemy));
            }
        }
    }

    private void ReviveEnemies() {
        foreach (var enemy in Enemies) {
            if (!enemy.activeSelf) {
                enemy.SetActive(true);
            }
        }
    }

    private IEnumerator ReviveEnemy(GameObject enemy) {
        yield return new WaitForSeconds(3);
        enemy.SetActive(true);
        enemy.GetComponent<EnemyUnit>().IsReviving = false;
    }
}
