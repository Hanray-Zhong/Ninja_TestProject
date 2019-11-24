using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeNode : MonoBehaviour
{
    private LineRenderer rope;
    private HingeJoint2D joint;
    private void Start() {
        rope = gameObject.GetComponent<LineRenderer>();
        joint = gameObject.GetComponent<HingeJoint2D>();
    }
    void Update()
    {
        if (joint.connectedBody != null) {
            rope.SetPosition(0, transform.position);
            rope.SetPosition(1, joint.connectedBody.transform.position);
        }
        
    }
}
