using UnityEngine;
using System.Collections;

public class CustomerWaiterController : MonoBehaviour {
	int i;
	GameObject employeeinfo;
	Employees employeevar;
	public int numofwaiters;
	GameObject[] customersarray;
	public Queue tableQueue;
	public GameObject customerObj;
	public GameObject waiterObj;
	GameObject instatiatedWaiter;
	WaiterController waiterc;
	public Vector3 customerVector;
	public Vector3 waiterVector;
	public bool generated = false;
	float elapsed;
	public Queue stateQueue;
	public Queue customerQueue;
	float numWaiting;
	public GameObject kitchen;
<<<<<<< HEAD
	public KitchenController kc;
	int numInLine;
	public string gameMode;
=======
	public GameObject worldinfo;
	public WorldVariables worldvar;
	int numofcustomers;
	int money = 0;
	int z;
>>>>>>> upstream/master
	//I seperated stateQueue and customerQueue (in WaiterController) since they have different priority

	void Start () {
		int z = 0;
		employeeinfo = GameObject.FindGameObjectWithTag ("Employees");
		employeevar = (Employees)employeeinfo.GetComponent (typeof(Employees));
		money = 8;
		i = 0;
		tableQueue = new Queue ();
		stateQueue = new Queue ();
		customerQueue = new Queue ();
		customerObj = (GameObject)Resources.Load ("Customer");
		waiterObj = (GameObject)Resources.Load ("Waiter");
		customerVector = new Vector3(0, 3f, 0);
		waiterVector = new Vector3(0, -3f, 0);
		elapsed = Time.time;
		worldinfo = GameObject.FindGameObjectWithTag ("WorldInfo");
		worldvar = (WorldVariables)worldinfo.GetComponent (typeof(WorldVariables));
		if(worldvar.getNumOfWaiters() > 0)
		{
		generateWaiters ();
		}
		initTableFinder ();
		kitchen = GameObject.FindGameObjectWithTag ("Kitchen");
		kc = (KitchenController) kitchen.GetComponent<KitchenController> ();
		numWaiting = 0;
<<<<<<< HEAD
		gameMode = "WaiterMiniGame"; //WaiterMiniGame
=======
		worldvar.switches ();
>>>>>>> upstream/master
	}

	// Update is called once per frame
	void Update () {
		if (Time.time - elapsed >= 4) {
			generated = true;
			elapsed = Time.time;
			if (getNumInLine () <= z) {
				generateCustomer();
			}
		}
	}

	public void setz()
	{
		z++;
	}
	

	void generateWaiters() {
		for(int i = 0; i < 10; i++)
		{
			string[] names = employeevar.getwaiternameArray();
			Debug.Log(names[i]);
			if(names[i] != null)
			{
				Vector3 v = waiterVector;
				v.y += Random.Range (0, 4);
				v.x += Random.Range (-1, 1);
				instatiatedWaiter = (GameObject)Instantiate(waiterObj, v, Quaternion.identity);
				waiterc = (WaiterController)instatiatedWaiter.GetComponent (typeof(WaiterController));
				waiterc.name = names[i];
			}
		}
	}

	void generateCustomer() {
		worldvar.setNumOfCustomers ();
		GameObject instantiatedCustomer = (GameObject)Instantiate(customerObj, customerVector, Quaternion.identity);
		instantiatedCustomer.SetActive (false);
		instantiatedCustomer.SetActive (true);
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
			if(c!= null)
			{
				CustomerController cc =(CustomerController) c.GetComponent(typeof(CustomerController));
				if (cc.getState() == "In Line") {
					cc.walkTo(new Vector3((float)(((float)current-1)/2)+.25f,2.5f,0),.5f);
					current++;
				}
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
			if(c!= null)
			{
				
				CustomerController cc = (CustomerController) c.GetComponent(typeof(CustomerController));
				if (cc.getState() == "In Line") {
					current++;
				}
				
			}
			
		}
		return current;
	}
}
