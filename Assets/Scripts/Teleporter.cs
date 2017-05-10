using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour {

	public GameObject TeleportMarker;
	public Transform Player;
	public float RayLength = 50f;

	// Use this for initialization
	void Start () {
		OVRTouchpad.Create ();
		OVRTouchpad.TouchHandler += TouchpadHandler;
	}
	
	// Update is called once per frame
	void Update () {
		Ray ray = new Ray (transform.position, transform.forward);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit, RayLength)) {
			if (hit.collider.tag == "Ground") {
				if (!TeleportMarker.activeSelf) {
					TeleportMarker.SetActive (true);
				}
				TeleportMarker.transform.position = hit.point;
			} else {
				TeleportMarker.SetActive (false);
			}
		} else {
			TeleportMarker.SetActive (false);
		}
	}

	void TouchpadHandler(object sender, System.EventArgs e) {

		OVRTouchpad.TouchArgs args = (OVRTouchpad.TouchArgs)e;
		if (args.TouchType == OVRTouchpad.TouchEvent.SingleTap) {
			// Need to check where the user is looking to teleport him
			if (TeleportMarker.activeSelf) {
				Vector3 markerPosition = TeleportMarker.transform.position;
				Player.position = new Vector3 (markerPosition.x, Player.position.y, 
					markerPosition.z);
			}
		}
	}
}
