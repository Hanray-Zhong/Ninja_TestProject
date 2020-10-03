using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace KanetoTools
{
    public class SceneTools : MonoBehaviour
    {
        /// <summary>
        /// 有延迟的加载场景
        /// </summary>
        /// <param name="SceneIndex"></param>
        /// <param name="delay"></param>
        public void LoadScene(int SceneIndex, float delay) {
		    StartCoroutine(Load(SceneIndex, delay));
	    }
        /// <summary>
        /// 没有延迟的加载场景
        /// </summary>
        /// <param name="SceneIndex"></param>
        public void LoadScene(int SceneIndex)
        {
            StartCoroutine(Load(SceneIndex, 1));
        }



        public void DontDestroyOnLoad(GameObject obj) {
            Object.DontDestroyOnLoad(obj);
        }


	    IEnumerator Load(int SceneIndex, float delay) {
            if (BGMController.Instance != null)
                BGMController.Instance.SceneTransition = true;
            if (PlayerSoundController.Instance != null)
                PlayerSoundController.Instance.StopAll();
		    yield return new WaitForSeconds(delay);
		    SceneManager.LoadScene(SceneIndex);
            if (BGMController.Instance != null)
                BGMController.Instance.IsChangeScene = true;
        }


        public void ExitGame() {
            Application.Quit();
        }
    }
}