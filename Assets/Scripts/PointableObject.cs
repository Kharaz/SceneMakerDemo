using UnityEngine;
using System.Collections;

public class PointableObject : StageObject {
	
	bool PointMode = false;
	public Transform pointTarget;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(PointMode){
			
		}
	}
	
	public void onSelect(){
	
	}
	
	public void TogglePointMode(){
		PointMode = !PointMode;
		pointTarget.gameObject.SetActive(PointMode);
		if(PointMode){
			pointTarget.position = transform.localPosition + transform.forward;
		}
	}
}
