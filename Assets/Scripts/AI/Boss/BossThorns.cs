using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossThorns : MonoBehaviour
{
    public GameObject explosionEffect;
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Ground") {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
        }
    }
}
