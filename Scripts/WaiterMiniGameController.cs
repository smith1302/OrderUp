using UnityEngine;
using System.Collections;

public class WaiterMiniGameController : MonoBehaviour {
	WalkingController wc;
	// Use this for initialization
	void Start () {
		wc = transform.GetComponent<WalkingController> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)){
			Vector3 pt = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			walkTo(pt);
		}
	}

	void walkTo(Vector3 v) {
		wc.goalCoord = v;
		wc.startWalk ();
	}
}
