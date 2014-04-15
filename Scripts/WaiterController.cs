using UnityEngine;
using System.Collections;

public class WaiterController : MonoBehaviour {
	WalkingController wc;
	CustomerWaiterController cwc;
	public float aboveTableBuffer;
	public float belowCustomerBuffer;
	private GameObject[] tables;
	private GameObject currentTarget;
	private GameObject currentCustomer;
	private CustomerController currentCC;
	private State currentState;
	int ordersToDeliver = 0;
	bool foodToDeliver = false;

	RaycastHit2D myhit = new RaycastHit2D();
	Ray2D myray = new Ray2D();
	// Use this for initialization
	void Start () {
		wc = transform.GetComponent<WalkingController> ();
		cwc = (GameObject.FindGameObjectWithTag ("Script")).GetComponent<CustomerWaiterController> ();
		wc.setState("None");
		belowCustomerBuffer = 1f;
		aboveTableBuffer = 1f;
	}
	
	// Update is called once per frame
	void Update () {
		stateAssigner ();
		//Debug.Log (wc.state);
		if (cwc.gameMode == "WaiterMiniGame" && Input.GetMouseButtonDown(0)){
			Vector3 pos = Input.mousePosition;
			Vector3 pt = Camera.main.ScreenToWorldPoint (pos);
			RaycastHit2D hitInfo = Physics2D.Raycast(pt, Vector2.zero);
			if(hitInfo != null) {
				string tag = "None";
				if (hitInfo.rigidbody != null) {
					tag = hitInfo.rigidbody.gameObject.tag;
				}else if (hitInfo.collider != null) {
					tag = hitInfo.collider.gameObject.tag;
				}
				Debug.Log(tag);
				if (tag == "Customer") {
					Debug.Log("Customer Click");
					currentTarget = (GameObject)cwc.customerQueue.Peek();
					currentCC = (CustomerController) currentTarget.GetComponent(typeof(CustomerController));
					wc.setState("GettingCustomer");
					if (!currentCC.hasWaiter) {
						currentCC.setWaiter(gameObject);
						currentCC.hasWaiter = true;
						currentCustomer = currentTarget;
						walkToTarget();
					}
					return;
				}else if (tag == "Table") {
					Debug.Log("Table Click");
					Properties p = (Properties)hitInfo.collider.gameObject.GetComponent(typeof(Properties));
					if (wc.state == "GettingCustomer" && p.getCustomer() == null) {
						currentTarget = hitInfo.collider.gameObject;
						cwc.customerQueue.Dequeue();
						cwc.shiftWaitingCustomers();
						startCustomerFollow();
						walkToTarget();
					}else if (p.getCustomer()) {
						currentCC = (CustomerController) p.getCustomer().GetComponent(typeof(CustomerController));
						Debug.Log(currentCC.getState());
						if (currentCC.getState() == "WaitAfterOrder") {
							currentTarget = hitInfo.collider.gameObject;
							walkToTarget();
							wc.setState("TakeOrder");
							ordersToDeliver++;
						}else if (currentCC.getState() == "WaitAfterFood" && foodToDeliver) {
							currentTarget = hitInfo.collider.gameObject;
							walkToTarget();
							wc.setState("deliverFoodToCustomer");
						}
					}
					return;
				}else if (tag == "Kitchen") {
					Debug.Log("Kitchen Click");
					currentTarget = hitInfo.collider.gameObject;
					walkToTarget();
					wc.setState("walkingToKitchen");
					return;
				}
			}
			walkTo(pt);
			wc.setState("None");
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

			if (cwc.gameMode != "WaiterMiniGame") {
				NonMiniGameModeStates();
			}else{
				WaiterMiniGameStates();
			}
		}
	}

	void WaiterMiniGameStates() {
		if (wc.state == "TakeOrder") {
			currentCC.setState("waitForFood");
		}
		if (wc.state == "walkingToKitchen") {
			if (ordersToDeliver > 0) {
				for (int i = 0; i < ordersToDeliver; i++) {
					cwc.kc.addOrder();
				}
				ordersToDeliver = 0;
			}
			if (cwc.kc.takeFood(foodToDeliver)) {
				foodToDeliver = true;
			}
			wc.setState("None");
		}
		if (wc.state == "deliverFoodToCustomer") {
			Properties p = (Properties) currentTarget.GetComponent(typeof(Properties));
			currentCC = (CustomerController) p.getCustomer().GetComponent(typeof(CustomerController));
			currentCC.setState("initEating");
			foodToDeliver = false;
		}
	}

	void NonMiniGameModeStates() {
		if (wc.state == "WalkToBase") {
			float randomY = Random.Range(-1,3);
			float randomX = Random.Range(-.5f,.5f);
			walkTo(new Vector3(randomX,randomY,0));
			currentTarget = null;
			wc.setState("None");
		}
		
		if (wc.state == "deliverFoodToCustomer") {
			Properties p = (Properties) currentTarget.GetComponent(typeof(Properties));
			currentCC = (CustomerController) p.getCustomer().GetComponent(typeof(CustomerController));
			currentCC.setState("initEating");
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
			currentTarget = cwc.kitchen;
			currentCC = (CustomerController) currentState.getOptionalObj().GetComponent(typeof(CustomerController));
			currentCC.setState("waitForFood");
			walkToTarget();
			wc.setState("deliverOrderToKitchen");
		}else
		if (wc.state == "walkingCustomer") {
			currentTarget = getNewTable();
			cwc.customerQueue.Dequeue();
			cwc.shiftWaitingCustomers();
			startCustomerFollow();
			walkToTarget();
			wc.setState("seatingCustomer");
			return;
		}else if (wc.state == "seatingCustomer") {
			wc.setState("WalkToBase");
			
		}else if (wc.state == "None") { //if we arent doing anything, check our state queue
			//Theres customers waiting and we aren't seating another customer AND theres available tables
			if (cwc.customerQueue.Count > 0 && wc.state != "walkingCustomer" && cwc.tableQueue.Count > 0) {
				currentTarget = (GameObject)cwc.customerQueue.Peek();
				currentCC = (CustomerController) currentTarget.GetComponent(typeof(CustomerController));
				
				if (!currentCC.hasWaiter) {
					Debug.Log("Getting customer.. waiter status: "+currentCC.hasWaiter);
					currentCC.setWaiter(gameObject);
					currentCC.hasWaiter = true;
					Debug.Log("Now has waiter...");
					currentCustomer = currentTarget;
					walkToTarget();
					wc.setState("walkingCustomer");
					return;
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

	public void startCustomerFollow() {
		if (currentTarget != null) {
			Vector3 n = currentTarget.transform.position;
			n.y += aboveTableBuffer;
			currentCC = (CustomerController)currentCustomer.GetComponent (typeof(CustomerController));
			currentCC.setTable (currentTarget);
			currentCC.walkTo (n, .35f);
			currentCC.setState ("walkToTable");
			wc.startWalk ();
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
