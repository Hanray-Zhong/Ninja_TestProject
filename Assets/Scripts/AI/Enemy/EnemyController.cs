using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject EnemyPrefab;
    public GameObject[] Enemies;
    public Vector2[] EnemiesPos;
    private bool canCoroutine = true;

    private void Awake() {
        for (int i = 0; i < Enemies.Length; i++) {
            EnemiesPos[i] = Enemies[i].transform.position;
        }
    }
    private void Update() {
        int i = 0;
        foreach (var enemy in Enemies) {
            if (enemy == null && canCoroutine) {
                canCoroutine = false;
                StartCoroutine(CreatNewEnemy(EnemyPrefab, EnemiesPos[i], i));
            }
            i++;
        }
    }


    IEnumerator CreatNewEnemy(GameObject EnemyPrefab, Vector2 Pos, int index) {
        yield return new WaitForSeconds(5);
        Enemies[index] = Instantiate(EnemyPrefab, Pos, Quaternion.identity, gameObject.transform);
        canCoroutine = true;
    }
}
