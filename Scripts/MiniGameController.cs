using UnityEngine;
using System.Collections;

public class MiniGameController : MonoBehaviour {
	public GameObject waiterObj;
	public Vector3 waiterVector;
	// Use this for initialization
	void Start () {
		//waiterObj = (GameObject)Resources.Load ("WaiterMiniGame");
		//generateWaiter ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void generateWaiter() {
		Vector3 v = waiterVector;
		v.y += Random.Range (0, 4);
		v.x += Random.Range (-1, 1);
		Instantiate(waiterObj, v, Quaternion.identity);
	}
}
