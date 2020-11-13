using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPanel : MonoBehaviour
{
    public float destroyDelay;

    private void Start()
    {
        Destroy(gameObject, destroyDelay);
    }
}
