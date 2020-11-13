using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanetoTools;

public class GameEndController : MonoBehaviour
{
    public SceneTools sceneTools;

    void Start()
    {
        StartCoroutine(ReturnToStart());
    }

    IEnumerator ReturnToStart()
    {
        yield return new WaitForSeconds(23);
        sceneTools.LoadScene(0, 1);
    }
}
