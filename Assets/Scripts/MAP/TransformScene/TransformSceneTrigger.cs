using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanetoTools;

public class TransformSceneTrigger : MonoBehaviour
{
    public SceneTools sceneTools;
    public int sceneIndex;
    public float transitionTime;
    public Animation TransitionBG;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (TransitionBG != null)
            {
                TransitionBG.Play("TransitionFadeUp");
            }
            PlayerController.Instance.isControlled = false;
            sceneTools.LoadScene(sceneIndex, transitionTime);
        }
    }
}
