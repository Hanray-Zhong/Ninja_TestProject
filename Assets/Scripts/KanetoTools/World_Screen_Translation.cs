using UnityEngine;

namespace KanetoTools
{
    public static class World_Screen_Translation {
        public static Vector3 WorldToScreen(GameObject mainCamera, Vector3 pos) {
            if (mainCamera.GetComponent<Camera>() == null) {
                Debug.Log("Error : The Camera conponent is null.");
                return Vector3.zero;
            }
            return mainCamera.GetComponent<Camera>().WorldToScreenPoint(pos);
        }
        public static Vector3 ScreenToWorld(GameObject mainCamera, Vector3 pos) {
            if (mainCamera.GetComponent<Camera>() == null) {
                Debug.Log("Error : The Camera conponent is null.");
                return Vector3.zero;
            }
            return mainCamera.GetComponent<Camera>().ScreenToWorldPoint(pos);
        }
    }
}