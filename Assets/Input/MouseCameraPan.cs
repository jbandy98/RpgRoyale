using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCameraPan : MonoBehaviour {

    public GameObject playerCamera;
    Vector3 lastMousePosition;
    Vector3 homePosition = new Vector3(2f, 0.5f, -10f);
    public float retractSpeed = 1.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 currMousePosition = Input.mousePosition;
        
        // middle mouse button will move the camera
		if (Input.GetMouseButton(2))
        {
            Debug.Log("Triggering camera move action.");
            Vector3 diff = currMousePosition - lastMousePosition;
            playerCamera.transform.Translate(-diff * Time.deltaTime);
            // don't let camera pan out of the players view
            if (playerCamera.transform.localPosition.x > homePosition.x + 2f)
            {
                Vector3 boundLoc = new Vector3(homePosition.x + 2f, playerCamera.transform.localPosition.y, playerCamera.transform.localPosition.z);
                playerCamera.transform.localPosition = boundLoc;
            }
            if (playerCamera.transform.localPosition.x < homePosition.x - 2f)
            {
                Vector3 boundLoc = new Vector3(homePosition.x-2f, playerCamera.transform.localPosition.y, playerCamera.transform.localPosition.z);
                playerCamera.transform.localPosition = boundLoc;
            }
            if (playerCamera.transform.localPosition.y > homePosition.y + 2f)
            {
                Vector3 boundLoc = new Vector3(playerCamera.transform.localPosition.x, homePosition.y + 2f, playerCamera.transform.localPosition.z);
                playerCamera.transform.localPosition = boundLoc;
            }
            if (playerCamera.transform.localPosition.y < homePosition.y - 2f)
            {
                Vector3 boundLoc = new Vector3(playerCamera.transform.localPosition.x, homePosition.y - 2f, playerCamera.transform.localPosition.z);
                playerCamera.transform.localPosition = boundLoc;
            }
        } else if (!Input.GetMouseButton(2))
        {
            // when the button isn't down, we need to slowly snap back to the origin
            Vector3 cameraOffset = homePosition - playerCamera.transform.localPosition;
            playerCamera.transform.Translate(cameraOffset * Time.deltaTime * retractSpeed);
        }

        lastMousePosition = Input.mousePosition;
	}
}
