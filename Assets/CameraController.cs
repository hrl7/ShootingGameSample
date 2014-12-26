using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	public GameObject target;
	private Vector3 diff;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt (target.transform.position + Vector3.up);
		if (Vector3.Distance (transform.position, target.transform.position) > 2f) {
			diff = Vector3.up * 2.0f + target.transform.position - transform.position;
			transform.position += diff * 0.01f;
		}
	}
}
