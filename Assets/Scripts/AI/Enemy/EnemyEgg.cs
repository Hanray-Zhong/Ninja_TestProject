using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEgg : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject explosionEffect;
    void Start() {
        StartCoroutine(GenerateEnemy());
    }
    private IEnumerator GenerateEnemy() {
        yield return new WaitForSeconds(3);
        Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        Instantiate(explosionEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
