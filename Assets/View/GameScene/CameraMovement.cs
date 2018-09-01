using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    Vector3 defaultPosition;
	// Use this for initialization
	void Start () {
        this.transform.position = new Vector3(0, 0, -10);
        defaultPosition = new Vector3(0, 0, -10);
	}
	
    public void UpdateCamera()
    {
        Debug.Log("Updating camera...Currently at: " + this.transform.localPosition.ToString());
        this.transform.position.Set(0f, 0f, -10f);
        Debug.Log("Camera position after set: " + this.transform.localPosition.ToString());
    }

	// Update is called once per frame
	void Update () {
        Debug.Log("camera check?" + this.transform.position.ToString());
		if (this.transform.localPosition != defaultPosition)
        {
            UpdateCamera();
        }
	}
}
