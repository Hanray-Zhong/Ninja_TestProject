using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // public GameObject EnemyPrefab;
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
            Invoke("ResurrectionEnemies", 1.5f);
            canInvoke = false;
        }
    }

    private void ResurrectionEnemies() {
        foreach (var enemy in Enemies) {
            if (!enemy.activeSelf) {
                enemy.SetActive(true);
            }
        }
    }

    // IEnumerator CreatNewEnemy(GameObject EnemyPrefab, Vector2 Pos, int index) {
    //     yield return new WaitForSeconds(5);
    //     Enemies[index] = Instantiate(EnemyPrefab, Pos, Quaternion.identity, gameObject.transform);
    //     canCoroutine = true;
    // }
}
