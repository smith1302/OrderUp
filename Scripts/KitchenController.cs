using UnityEngine;
using System.Collections;

public class KitchenController : MonoBehaviour {
	public Queue orders;
	int ordersReady;
	// Use this for initialization
	void Start () {
		ordersReady = 0;
		orders = new Queue ();
	}

	public void addOrder() {
		orders.Enqueue (1);
		Debug.Log("Kitchen has taken order");
	}

	public bool takeFood(bool hasFoodAlready) {
		bool toReturn = false;
		if (ordersReady > 0 && !hasFoodAlready) {
			toReturn = true;
			ordersReady--;
		}
		return toReturn;
	}
	// Update is called once per frame
	void Update () {
		if (orders.Count != 0) {
			StartCoroutine(makeOrderReady());
		}
	}

	IEnumerator makeOrderReady() {
		yield return new WaitForSeconds(2);
		if (orders.Count != 0) {
				orders.Dequeue ();
				ordersReady++;
				Debug.Log ("order ready! total: " + ordersReady);
		}
	}

	void OnGUI()
	{
		// Make a background box
		if (ordersReady > 0) {
			GUI.TextField (new Rect (transform.position.x+20, transform.position.y, 150, 50), "Orders Ready: "+ordersReady);
		}
		
	}
}
