using UnityEngine;
using System.Collections;

public class UserController : MonoBehaviour
{
	public Transform currentCamera;
	
	public Transform selectedTrans;
	
	public Transform Camera1;
	public Transform Camera2;
	public Transform Camera3;
	
	float moveSpeed = 5.0f;
	//float rotationSpeed = 20f;
	float mSensitivity = 5f;
	
	float yRotation = 0;
	float xRotation = 0;
	
	bool freeCam = true;

	// Use this for initialization
	void Start ()
	{
		yRotation = transform.rotation.y;
		xRotation = transform.rotation.x;
		
		SwitchCamera(1);
	}

	// Update is called once per frame
	void Update ()
	{	
		freeCamCheck();
		
		if(freeCam){
			DoKeyPresses();
			DoMouseMovement();
		}
		
		DoSelection();
	}
	
	void DoKeyPresses(){
		if(Input.GetKey(KeyCode.W)){
			transform.parent.localPosition += transform.forward * moveSpeed * Time.deltaTime;
		}
		if(Input.GetKey(KeyCode.S)){
			transform.parent.localPosition -= transform.forward * moveSpeed * Time.deltaTime;
		}
		if(Input.GetKey(KeyCode.A)){
			transform.parent.localPosition -= transform.right * moveSpeed * Time.deltaTime;
		}
		if(Input.GetKey(KeyCode.D)){
			transform.parent.localPosition += transform.right * moveSpeed * Time.deltaTime;
		}			
	}
	
	void freeCamCheck(){
		if(Input.GetKeyDown(KeyCode.Escape)){
			freeCam = !freeCam;
			Debug.Log ("Freecam changed to " + freeCam);
		}
	
		Screen.lockCursor = freeCam;
	}
	
	void DoMouseMovement(){
			yRotation += Input.GetAxis("Mouse Y") * mSensitivity;
			xRotation += Input.GetAxis("Mouse X") * mSensitivity;
			
			transform.parent.rotation = Quaternion.Euler(-yRotation,xRotation,0);
			//Debug.Log ("xRot: " + xRotation + "," + "yRot: " + yRotation);
	}
	
	void DoSelection(){
		if(Input.GetMouseButtonDown(0)){
			Deselect();
			Debug.Log ("Clicked mouse 0");
			
			Ray ray = currentCamera.camera.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			
			if(!Physics.Raycast (ray, out hit)){
				return;
			}
			
			Debug.Log ("Hit thing: " + hit.transform.name);
			if(hit.transform.parent.GetComponent<PointableObject>() && hit.transform.parent.gameObject.transform != selectedTrans){
				GameObject selected = hit.transform.parent.gameObject;
				selectedTrans = selected.GetComponent<PointableObject>().Select();
			}
			
			if(selectedTrans && Input.GetMouseButton(0)){
				//DragSelected();
			} 
		}
	}
	
	void DragSelected(){
		Vector3 mousePos = Input.mousePosition;
		selectedTrans.position += currentCamera.camera.ScreenToWorldPoint(mousePos)*Time.deltaTime;
		
	}
	
	public void Deselect(){
		selectedTrans = null;
	}
	
	public void SwitchCamera(int camNum){
		//currentCamera.parent = null;
		
		if(camNum == 1){
			currentCamera = Camera1;
			
			Camera2.camera.enabled = false;
			Camera3.camera.enabled = false;
			
			Camera2.gameObject.GetComponent<Camera>().enabled = false;
			Camera3.gameObject.GetComponent<Camera>().enabled = false;
		}
		if(camNum == 2){
			currentCamera = Camera2;
			
			Camera1.camera.enabled = false;
			Camera3.camera.enabled = false;
			
			Camera1.gameObject.GetComponent<Camera>().enabled = false;
			Camera3.gameObject.GetComponent<Camera>().enabled = false;
		} 
		if(camNum == 3){
			currentCamera = Camera3;
			
			Camera1.camera.enabled = false;
			Camera2.camera.enabled = false;
			
			Camera1.gameObject.GetComponent<Camera>().enabled = false;
			Camera2.gameObject.GetComponent<Camera>().enabled = false;
		}
		
		transform.position = currentCamera.position;
		transform.rotation = currentCamera.rotation;
		
		//currentCamera.parent.parent = transform;
		transform.parent = currentCamera.transform.parent;
		currentCamera.camera.enabled = true;
		currentCamera.gameObject.GetComponent<Camera>().enabled = true;
	}
}

