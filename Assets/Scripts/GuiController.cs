using UnityEngine;
using System.Collections;

public class GuiController : MonoBehaviour
{
	UserController user;

	// Use this for initialization
	void Start ()
	{
		user = transform.GetComponent<UserController>();
	}

	// Update is called once per frame
	void Update ()
	{

	}
	
	void OnGUI(){
		CameraButtons();
		DrawSelectedContext();
	}
	
	void CameraButtons(){
		if(GUI.Button(new Rect(0,0,100,100), "Camera1")){
			user.SwitchCamera(1);
		}
		if(GUI.Button(new Rect(0,100,100,100), "Camera2")){
			user.SwitchCamera(2);
		}
		if(GUI.Button(new Rect(0,200,100,100), "Camera3")){
			user.SwitchCamera(3);
		}
	}
	
	void DrawSelectedContext() {
		if(user.selectedTrans){
			Vector3 transPos = user.selectedTrans.position;
			Vector3 camPoint = user.currentCamera.camera.WorldToScreenPoint(transPos);

			Rect newButton = new Rect(camPoint.x-25, Screen.height- camPoint.y-25, 50, 50);
			if(GUI.Button (newButton, "point")){
				user.selectedTrans.GetComponent<PointableObject>().TogglePointMode();
			}
		}
	}
}

