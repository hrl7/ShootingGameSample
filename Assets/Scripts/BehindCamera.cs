using UnityEngine;
using System.Collections;

public class ViewerCamera : MonoBehaviour {
	
	public GameObject viewObject = null;
	
	public float rotationSensitivity = 0.01f;
	public float distanceSensitivity = 0.01f;
	public float followObjectSmooth = 3f;
	public float maxRotationY = 0.45f;
	public float minRotationY = -0.45f;
	public float minDistance = 0.5f;
	public float maxDistance = 5f;
	
	public float defaultDistance = 2f;
	public float defaultAngularPositionX = 0f;
	public float defaultAngularPositionY = 0f;
	
	protected float distance = 0f;
	protected Vector2 cameraPosParam = Vector2.zero;
	
	private Vector3 clickedPos = Vector3.zero;
	private int clickedFlag = 0; //0:none 1:left 2:right
	private Vector3 pivotTemp = Vector3.zero;
	private float distanceTemp = 0f;
	private Vector2 cameraPosParamTemp = Vector2.zero;
	// Use this for initialization
	void Start () {
		 distance =defaultDistance;
		 cameraPosParam = new Vector2 ( defaultAngularPositionX / 180f * Mathf.PI,defaultAngularPositionY / 180f * Mathf.PI);
		 pivotTemp =transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if ( clickedFlag == 0) {
			if (Input.GetMouseButtonDown(0)) {
				 clickedPos = Input.mousePosition;
				 cameraPosParamTemp =cameraPosParam;
				 clickedFlag = 1;
			}
		}
		
		if ( clickedFlag == 0) {
			if (Input.GetMouseButtonDown(1)) {
				 clickedPos = Input.mousePosition;
				 distanceTemp =distance;
				 clickedFlag = 2;
			}
		}
		
		if ( clickedFlag == 1 && Input.GetMouseButtonUp(0)) {
			 clickedFlag = 0;
		}
		
		if ( clickedFlag == 2 && Input.GetMouseButtonUp(1)) {
			 clickedFlag = 0;
		}
		
		Vector3 mousePosDistance = Input.mousePosition -clickedPos;
		
		switch ( clickedFlag) {
		case 1:
			var diff = new Vector2 (mousePosDistance.x, -mousePosDistance.y) * rotationSensitivity;
			 cameraPosParam.x =cameraPosParamTemp.x + diff.x;
			 cameraPosParam.y = Mathf.Clamp( cameraPosParamTemp.y + diff.y,minRotationY * Mathf.PI,maxRotationY * Mathf.PI);
			break;
		case 2:
			 distance = Mathf.Clamp ( distanceTemp + mousePosDistance.y *distanceSensitivity,minDistance,maxDistance);
			break;
		}
		
		Vector3 orbitPos = GetOrbitPosition ( cameraPosParam,distance);
		
		Vector3 pivot = Vector3.Lerp( pivotTemp,viewObject.transform.position, Time.deltaTime *followObjectSmooth);
		 transform.position = pivot + orbitPos;
		 transform.LookAt ( viewObject.transform);
		
		 pivotTemp = pivot;
	}
	
	private Vector3 GetOrbitPosition(Vector2 anglarParam, float distance){
		float x = Mathf.Sin (anglarParam.x) * Mathf.Cos (anglarParam.y);
		float z = Mathf.Cos (anglarParam.x) * Mathf.Cos (anglarParam.y);
		float y = Mathf.Sin (anglarParam.y);
		
		return new Vector3 (x, y, z) * distance;
	}
}