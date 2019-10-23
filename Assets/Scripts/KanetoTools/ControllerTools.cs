using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KanetoTools
{
    public class ControllerTools : MonoBehaviour
    {
        public Vector2 AixsInputConfiguration(string hl_name, string vt_name) {
            Vector2 input = new Vector2(Input.GetAxis(hl_name), Input.GetAxis(vt_name));
            return input;
        }
        public Vector2 RawAixsInputConfiguration(string hl_name, string vt_name) {
            Vector2 input = new Vector2(Input.GetAxisRaw(hl_name), Input.GetAxisRaw(vt_name));
            return input;
        }
        public float InteractionConfiguration(string Button_name) {
            float interaction = Input.GetAxis(Button_name);
            return interaction;
        }
    }
}

