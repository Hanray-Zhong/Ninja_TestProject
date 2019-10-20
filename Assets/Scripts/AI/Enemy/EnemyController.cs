using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject EnemyPrefab;
    public GameObject[] Enemies;
    public Vector2[] EnemiesPos;
    private bool allowCreat;

    private void Awake() {
        for (int i = 0; i < Enemies.Length; i++) {
            EnemiesPos[i] = Enemies[i].transform.position;
        }
    }
    private void Update() {
        foreach (var enemy in Enemies) {
            int i = 0;
            if (enemy == null)
                allowCreat = true;
            if (allowCreat) {
                StartCoroutine(CreatNewEnemy(EnemyPrefab, EnemiesPos[i], i));
            }
            i++;
        }
    }


    IEnumerator CreatNewEnemy(GameObject EnemyPrefab, Vector2 Pos, int index) {
        yield return new WaitForSeconds(5);
        Enemies[index] = Instantiate(EnemyPrefab, Pos, Quaternion.identity, gameObject.transform);
        allowCreat = false;
        StopCoroutine("CreatNewEnemy");
    }
}
