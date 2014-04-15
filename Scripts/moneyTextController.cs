using UnityEngine;
using System.Collections;

public class moneyTextController : MonoBehaviour {
	float originY;
	// Use this for initialization
	void Start () {
		originY = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3 (transform.position.x, transform.position.y+.02f, transform.position.z);
		float diff = transform.position.y - originY;
		Color color = renderer.material.color;
		color.a -= 0.02f;
		renderer.material.color = color;
		if (diff > 1f) {
			Debug.Log("Erase");
			Destroy(gameObject);
		}
	}
}
