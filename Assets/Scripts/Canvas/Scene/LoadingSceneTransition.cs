using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanetoTools;

public class LoadingSceneTransition : MonoBehaviour
{
    private bool canFade;
    public bool CanFade {
        get {return canFade;}
        set {canFade = value;}
    }

    private float FadeSpeed = 0;
    void Update() {
        Fade(gameObject, 0);
    }

    public void Fade(GameObject self, float PresetFadeSpeed) {
			CanvasGroup canvasGroup = self.GetComponent<CanvasGroup>();
			if (canvasGroup == null) {
				Debug.Log("Fade Error : CanvasGroup is null.");
				return;
			}
			// if (PresetFadeSpeed == 0) {
			// 	Debug.Log("Fade Error : Preset Fade Speed can't be 0.");
			// 	return;
			// }
        	if (CanFade) {
				if (canvasGroup.alpha == 0) {
					if (PresetFadeSpeed == 0)
						FadeSpeed = 0.03f;
					else
						FadeSpeed = PresetFadeSpeed;
				}
				if (canvasGroup.alpha == 1) {
					if (PresetFadeSpeed == 0)
						FadeSpeed = -0.03f;
					else
						FadeSpeed = -PresetFadeSpeed;
				}
			}
			else {
				FadeSpeed = 0;
			}
			canvasGroup.alpha += FadeSpeed;
			if (CanFade && (canvasGroup.alpha == 0 || canvasGroup.alpha == 1)) {
				CanFade = false;
			}
   	 	}
}
