using UnityEngine;
using System.Collections;

public class ManagerWaiterController : MonoBehaviour {
	WalkingController wc;
	ManagerCustomerWaiterController cwc;
	public float aboveTableBuffer;
	public float belowCustomerBuffer;
	private GameObject lazywaiter;
	private GameObject instantiatedLazy;
	private GameObject[] tables;
	private GameObject currentTarget;
	private GameObject currentCustomer;
	private ManagerCustomerController currentCC;
	private State currentState;
	double lazy;
	bool lazytrue;
	string prevState;
	int k;
	// Use this for initialization
	void Start () {
		k = 0;
		wc = transform.GetComponent<WalkingController> ();
		cwc = (GameObject.FindGameObjectWithTag ("Script")).GetComponent<ManagerCustomerWaiterController> ();
		wc.setState("None");
		belowCustomerBuffer = 1f;
		aboveTableBuffer = 1f;
		lazy = Random.Range (0, 30);
		lazywaiter = (GameObject)Resources.Load ("Lazy");
	}
	
	// Update is called once per frame
	void Update () {
		if(cwc.Begin)
		{
		if(!lazytrue)
		{
			stateAssigner ();
		}
		int headsortails = Random.Range (0, 2);
		if (lazy < 150 && headsortails == 1 ) 
		{
			lazy += .5;
		}
		if (lazy < 150 && headsortails == 0 ) 
		{
			lazy -= .4;
		}
		if(lazy > 150)
		{
			lazy = 150;
		}
		if(lazy < 0)
		{
			lazy = 0;
		}
		
		if(lazy >= 100 && lazytrue == false)
		{
			generateLazy(transform.position);
			lazytrue = true;
			Vector3 leftposition = transform.position;
			leftposition.x += .5f;
			walkTo(leftposition);
			
			
		}
		if(lazy < 100 && lazytrue == true)
		{
			Destroy(instantiatedLazy);
			lazytrue = false;
		}
		if(lazytrue)
		{
			Vector3 myposition = transform.position;
			myposition.y += .7f;
			instantiatedLazy.transform.position = myposition;
		}
		
	}
	}
	void walkToTarget() {
		if (currentTarget == null) {
			wc.setState("None");
			return;
		}
		Vector3 newPos = currentTarget.transform.position;
		if (currentTarget.tag == "Table") {
			newPos.y += aboveTableBuffer;
		} else if (currentTarget.tag == "Customer" || currentTarget.tag == "Kitchen") {
			newPos.y -= belowCustomerBuffer;
		}
		wc.goalCoord = newPos;
		wc.startWalk ();
	}
	
	void walkTo(Vector3 v) {
		wc.goalCoord = v;
		wc.startWalk ();
	}
	
	void stateAssigner() {
		//IMPORTANT: Fired AFTER we finished the specified state
		if (!wc.hasTarget) {
			
			if (wc.state == "WalkToBase") {
				float randomY = Random.Range(-1,3);
				float randomX = Random.Range(-.5f,.5f);
				walkTo(new Vector3(randomX,randomY,0));
				currentTarget = null;
				wc.setState("None");
			}
			
			if (wc.state == "deliverFoodToCustomer") {
				Properties p = (Properties) currentTarget.GetComponent(typeof(Properties));
				if(p != null)
				{
				if(p.getCustomer() != null)
				{
				currentCC = (ManagerCustomerController) p.getCustomer().GetComponent(typeof(ManagerCustomerController));
				currentCC.setState("initEating");
				}
				}
				wc.setState("WalkToBase");
			}else
			if (wc.state == "getFood") {// just go to the kitchen
				currentTarget = currentState.getOptionalObj();
				walkToTarget();
				wc.setState("deliverFoodToCustomer");
			}else
			if (wc.state == "deliverOrderToKitchen") {
				//State obj = table
				//State optional obj = customer
				State s = new State(cwc.kitchen, "getFood");
				s.setOptionalObj (currentState.getObj());
				cwc.stateQueue.Enqueue (s);
				wc.setState("None");
			}else
			if (wc.state == "readyToOrder") {
				if(currentCC != null)
				{
				currentTarget = cwc.kitchen;
				if(currentCC != null)
				{
				if(currentState != null)
				{
				if(currentState.getOptionalObj() != null)
				{
				currentCC = (ManagerCustomerController) currentState.getOptionalObj().GetComponent(typeof(ManagerCustomerController));
				currentCC.setState("waitForFood");
				walkToTarget();
				wc.setState("deliverOrderToKitchen");
				}
				}
				}
				else
				{
					currentTarget = null;
					wc.setState("None");
				}
				}
				else
				{
					wc.setState("None");
				}

			}else
			if (wc.state == "walkingCustomer") {

				currentTarget = getNewTable();
				startCustomerFollow();
				cwc.customerQueue.Dequeue();
				cwc.shiftWaitingCustomers();
				walkToTarget();
				wc.setState("seatingCustomer");
				return;
			}else if (wc.state == "seatingCustomer") {
				wc.setState("WalkToBase");
				
			}else if (wc.state == "None") { //if we arent doing anything, check our state queue
				//Theres customers waiting and we aren't seating another customer AND theres available tables
				if (cwc.customerQueue.Count > 0 && wc.state != "walkingCustomer" && cwc.tableQueue.Count > 0) {
					currentTarget = (GameObject)cwc.customerQueue.Peek();
					if(currentTarget != null)
					{
					currentCC = (ManagerCustomerController) currentTarget.GetComponent(typeof(ManagerCustomerController));

					if (!currentCC.hasWaiter) {
						currentCC.setWaiter(gameObject);
						currentCC.hasWaiter = true;
						currentCustomer = currentTarget;
						walkToTarget();
						wc.setState("walkingCustomer");
						return;
					}

					}
				}
				if (cwc.stateQueue.Count > 0) {
					currentState = (State)cwc.stateQueue.Dequeue();
					currentTarget = currentState.getObj();
					walkToTarget();
					wc.setState(currentState.getState());
					return;
				}
			}else{
				wc.setState("WalkToBase");
			}
		}
	}
	
	public void generateLazy(Vector3 v) {
		v.y += 1f;
		v.z = -1f;
		instantiatedLazy = (GameObject)Instantiate(lazywaiter, v, Quaternion.identity);
		
	}
	
	public void reduceLazy(int x)
	{
		lazy += -x;
	}
	
	public void startCustomerFollow() {
		if (currentTarget != null) {
			Vector3 n = currentTarget.transform.position;
			n.y += aboveTableBuffer;
			if(currentCustomer!= null)
			{
			currentCC = (ManagerCustomerController)currentCustomer.GetComponent (typeof(ManagerCustomerController));
			currentCC.setTable (currentTarget);
			currentCC.walkTo (n, .25f);
			currentCC.setState ("walkToTable");
			wc.startWalk ();
			}
		} else {
			wc.setState("None");
		}
	}
	
	public GameObject getNewTable() {
		if (cwc.tableQueue.Count == 0) {
			return null;
		}
		GameObject table = (GameObject)cwc.tableQueue.Dequeue ();
		Properties p = (Properties)table.GetComponent (typeof(Properties));
		p.open = false;
		return table;
	}
}

