using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace KanetoTools
{
    public class SceneTools : MonoBehaviour
    {
        public void LoadScene(int SceneIndex, float delay) {
		    StartCoroutine(Load(SceneIndex, delay));
	    }
        public void DontDestroyOnLoad(GameObject obj) {
            Object.DontDestroyOnLoad(obj);
        }
	    IEnumerator Load(int SceneIndex, float delay) {
		    yield return new WaitForSeconds(delay);
		    SceneManager.LoadScene(SceneIndex);
	    }



        public void ExitGame() {
            Application.Quit();
        }
    }
}