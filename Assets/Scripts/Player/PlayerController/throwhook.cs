using UnityEngine;
using System.Collections;

public class throwhook : MonoBehaviour {


	public GameObject hook;
	private bool ropeActive;
	private GameObject curHook;
	
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			if (ropeActive == false) {
				Vector2 destiny = Camera.main.ScreenToWorldPoint (Input.mousePosition);
				curHook = (GameObject)Instantiate (hook, transform.position, Quaternion.identity);
				curHook.GetComponent<Rope>().player = gameObject;
				curHook.GetComponent<Rope>().destination = destiny;
				ropeActive = true;
			} else {
				//delete rope
				Destroy (curHook);
				ropeActive = false;
			}
		}
	}
}
