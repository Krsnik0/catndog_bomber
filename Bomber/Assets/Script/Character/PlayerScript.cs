using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	private const int L_MOUSE = 0;
	private const int R_MOUSE = 1;
	private const int MID_MOUSE = 2;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetMouseButtonDown (L_MOUSE)) {
			Vector3 mousePosition = Input.mousePosition;

			Camera mainCam = Camera.main;
			Vector3 camPos = Camera.main.transform.position;

			camPos.x += mainCam.pixelWidth * 0.5f;
			camPos.y += mainCam.pixelHeight * 0.5f;

			mousePosition.x -= camPos.x;
			mousePosition.y -= camPos.y;

			Debug.Log( mousePosition );
		}
	}
}
