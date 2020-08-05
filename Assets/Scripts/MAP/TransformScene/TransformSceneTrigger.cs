using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanetoTools;

public class TransformSceneTrigger : MonoBehaviour
{
    public SceneTools sceneTools;
    public int sceneIndex;
    public float transitionTime;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            sceneTools.LoadScene(sceneIndex, transitionTime);
        }
    }
}
