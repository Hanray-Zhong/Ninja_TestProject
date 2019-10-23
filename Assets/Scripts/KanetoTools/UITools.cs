using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace KanetoTools
{
	public class UITools : MonoBehaviour
	{
    	public void Fade(GameObject self, CanvasGroup canvasGroup, bool CanFade, float PresetFadeSpeed, float FadeSpeed) {
			canvasGroup = self.GetComponent<CanvasGroup>();
			if (canvasGroup == null) {
				Debug.Log("Fade Error : CanvasGroup is null.");
				return;
			}
			if (PresetFadeSpeed == 0) {
				Debug.Log("Fade Error : Preset Fade Speed can't be 0.");
				return;
			}
        	if (CanFade) {
				if (canvasGroup.alpha == 0) {
					if (PresetFadeSpeed == 0)
						FadeSpeed = 0.03f;
					else
						FadeSpeed = PresetFadeSpeed;
				}
				else if (canvasGroup.alpha == 1) {
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
}

