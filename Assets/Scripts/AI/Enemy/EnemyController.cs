using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public List<GameObject> Enemies = new List<GameObject>();
    public List<Vector2> EnemiesPos = new List<Vector2>();

    public PlayerUnit playerUnit;
    private bool canInvoke = true;

    private void Awake() {
        GameObject[] allEnemies;
        allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in allEnemies) {
            if (enemy.GetComponent<EnemyUnit>().canBeResurrected) {
                Enemies.Add(enemy);
                EnemiesPos.Add(enemy.transform.position);
            }
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
