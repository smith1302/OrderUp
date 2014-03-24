using UnityEngine;
using System.Collections;

public class CustomerWaiterController : MonoBehaviour {
	public Queue tableQueue;
	public GameObject customerObj;
	public GameObject waiterObj;
	public Vector3 customerVector;
	public Vector3 waiterVector;
	public bool generated = false;
	float elapsed;
	public Queue stateQueue;
	public Queue customerQueue; 
	public Queue customerLine;
	float numWaiting;
	public GameObject kitchen;
	int numInLine;
	public string gameMode;
	//I seperated stateQueue and customerQueue (in WaiterController) since they have different priority

	void Start () {
		tableQueue = new Queue ();
		stateQueue = new Queue ();
		customerQueue = new Queue ();
		customerLine = new Queue ();
		waiterObj = (GameObject)Resources.Load ("Waiter");
		customerVector = new Vector3(0, 3f, 0);
		waiterVector = new Vector3(0, -3f, 0);
		elapsed = Time.time;
		initTableFinder ();
		generateWaiter ();
		kitchen = GameObject.FindGameObjectWithTag ("Kitchen");
		numWaiting = 0;
		gameMode = "WaiterMiniGame";
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time - elapsed >= 3) {
			generated = true;
			elapsed = Time.time;
			if (getNumInLine () <= 3) {
				generateCustomer();
			}
		}
	}

	void generateWaiter() {
		Vector3 v = waiterVector;
		v.y += Random.Range (0, 4);
		v.x += Random.Range (-1, 1);
		Instantiate(waiterObj, v, Quaternion.identity);
	}

	void generateCustomer() {
		Instantiate(customerObj, customerVector, Quaternion.identity);
		numWaiting++;
		/*GameObject waiter = GameObject.FindGameObjectWithTag ("Waiter");
		if (waiter != null) {
			WaiterController wc = (WaiterController)waiter.GetComponent(typeof(WaiterController));
			wc.setTarget(instantiatedCustomer);
		}*/
	}

	GameObject getRandomWaiter() {
		foreach(GameObject fooObj in GameObject.FindGameObjectsWithTag("Table"))
		{
			return fooObj;
		}
		return null;
	}

	IEnumerator wait(int num) {
		yield return new WaitForSeconds(num);
	}

	public void addTableToQueue(GameObject t) {
		Properties p = (Properties) t.GetComponent(typeof(Properties));
		p.open = true;
		tableQueue.Enqueue(t);
	}

	public int initTableFinder() {
		//find and populate our tables array with table objects
		int count = 0;
		foreach(GameObject table in GameObject.FindGameObjectsWithTag("Table"))
		{
			count++;
			addTableToQueue(table);
		}
		return count;
	}

	public void shiftWaitingCustomers() {
		Queue temp = new Queue (customerQueue);
		int current = 0;
		while (temp.Count != 0) {
			GameObject c = (GameObject)temp.Dequeue();
			CustomerController cc =(CustomerController) c.GetComponent(typeof(CustomerController));
			if (cc.getState() == "In Line") {
				cc.walkTo(new Vector3((float)(((float)current-1)/2)+.25f,2.5f,0),.5f);
				current++;
			}
		}
	}

	public int getNumInLine() {
		if (customerQueue == null || customerQueue.Count == 0) {
			return 0;
		}
		Queue temp = new Queue (customerQueue);
		int current = 0;
		while (temp.Count != 0) {
			GameObject c = (GameObject)temp.Dequeue();
			CustomerController cc =(CustomerController) c.GetComponent(typeof(CustomerController));
			if (cc.getState() == "In Line") {
				current++;
			}
		}
		return current;
	}
}
