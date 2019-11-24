using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    public Vector2 destination {get; set;}
	public float generationSpeed;
	public float nodeSpacing;
	public GameObject player {get; set;}
	public GameObject nodePrefab;
	private GameObject lastNode;
	private List<GameObject> Nodes = new List<GameObject>();
	private bool isDone = false;
    [Header("LineRenderer")]
    private LineRenderer lineRenderer;
    int vertexCount = 2;

	void Start () {
		lastNode = transform.gameObject;
        Nodes.Add (transform.gameObject);
        lineRenderer = GetComponent<LineRenderer>();
	}
	
	void Update () {
        CreateRope();
        RenderRope();
    }
    void CreateRope() {
        if (nodeSpacing == 0) {
            Debug.LogError("Rope node spacing can't be 0");
            return;
        }
		transform.position = Vector2.MoveTowards (transform.position, destination, generationSpeed);
		if ((Vector2)transform.position != destination) {
			if (Vector2.Distance (player.transform.position, lastNode.transform.position) > nodeSpacing) {
				CreateNode ();
			}
		} 
        else if (isDone == false) {
			isDone = true;
			while(Vector2.Distance (player.transform.position, lastNode.transform.position) > nodeSpacing) {
				CreateNode ();
			}
			lastNode.GetComponent<HingeJoint2D> ().connectedBody = player.GetComponent<Rigidbody2D> ();
		}
    }
	void CreateNode() {
		Vector2 dir = (player.transform.position - lastNode.transform.position).normalized;
		Vector2 generatePos = dir * nodeSpacing + (Vector2)lastNode.transform.position;
		GameObject node = (GameObject)Instantiate (nodePrefab, generatePos, Quaternion.identity);
		node.transform.SetParent (transform);
		lastNode.GetComponent<HingeJoint2D> ().connectedBody = node.GetComponent<Rigidbody2D> ();
		lastNode = node;
		Nodes.Add (lastNode);
        vertexCount++;
	}
    void RenderRope() {
        if (lineRenderer == null) {
            Debug.LogError("LineRenderer is null.");
            return;
        }
		lineRenderer.positionCount = vertexCount;
		int i;
		for (i = 0; i < Nodes.Count; i++) {
			lineRenderer.SetPosition (i, Nodes [i].transform.position);
		}
		lineRenderer.SetPosition (i, player.transform.position);

	}
}
