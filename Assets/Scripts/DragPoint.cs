using UnityEngine;
using System.Collections;

public class DragPoint : MonoBehaviour {

	Vector3 screenPoint;
	Vector3 offset;

	Vector3 currScreenPoint;
	Vector3 currPosition;
	
	UserController user;

	// Use this for initialization
	void Start () {
		user = GameObject.Find("UserController").GetComponent<UserController>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnMouseDown(){
		screenPoint = Camera.main.WorldToScreenPoint(transform.position);
		offset = transform.position - user.currentCamera.camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
	}
	
	void OnMouseDrag(){
		currScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
		currPosition = Camera.main.ScreenToWorldPoint(currScreenPoint) + offset;
		transform.position = currPosition;
		
		transform.parent.LookAt(transform.position);
	}
}
