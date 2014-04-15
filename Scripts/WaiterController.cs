﻿using UnityEngine;
using System.Collections;

public class WaiterController : MonoBehaviour {
	WalkingController wc;
	CustomerWaiterController cwc;
	ChefController cfc;
	public float aboveTableBuffer;
	public float belowCustomerBuffer;
	private GameObject[] tables;
	private GameObject currentTarget;
	private GameObject currentCustomer;
	private CustomerController currentCC;
	private GameObject prevTarget;
	private State currentState;
	Queue tablequeue;
	GameObject employeeinfo;
	Employees employeevar;
	public string name;
	int index;
	GameObject worldinfo;
	WorldVariables worldvar;
	GameObject restaurantinfo;
	RestaurantGUI restaurantvar;
	// Use this for initialization
	void Start () {
		tablequeue = new Queue ();
		restaurantinfo = GameObject.FindGameObjectWithTag ("MainCamera");
		restaurantvar = (RestaurantGUI)restaurantinfo.GetComponent (typeof(RestaurantGUI));
		worldinfo = GameObject.FindGameObjectWithTag ("WorldInfo");
		worldvar = (WorldVariables)worldinfo.GetComponent (typeof(WorldVariables));
		wc = transform.GetComponent<WalkingController> ();
		cwc = (GameObject.FindGameObjectWithTag ("Script")).GetComponent<CustomerWaiterController> ();
		cfc = (GameObject.FindGameObjectWithTag ("Kitchen")).GetComponent<ChefController> ();
		wc.setState("None");
		belowCustomerBuffer = 1f;
		aboveTableBuffer = 1f;
		if(restaurantvar.newwaiter == true)
		{
		employeeinfo = GameObject.FindGameObjectWithTag ("Employees");
		employeevar = (Employees)employeeinfo.GetComponent (typeof(Employees));
		name = employeevar.createName ();
		index = employeevar.setwaiternameArray (name);
		Debug.Log(index + " " + name);
		restaurantvar.newwaiter = false;
		}

	}
	
	// Update is called once per frame
	void Update () {
		stateAssigner ();
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
				Debug.Log(currentTarget);
				Properties p = (Properties) currentTarget.GetComponent(typeof(Properties));
				Debug.Log(p);
				Debug.Log(currentTarget);
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
				cfc.setState("GotOrder");

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
					if(currentTarget != null)
					{
						currentCC = (CustomerController) currentTarget.GetComponent(typeof(CustomerController));
						
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
				if(cfc.orderready && tablequeue.Count > 0)
				{
					State s = new State(cwc.kitchen, "getFood");
					s.setOptionalObj ((GameObject) tablequeue.Dequeue());
					cwc.stateQueue.Enqueue (s);
					cfc.orderready = false;
					cfc.setonce = false;
					return;
				}
				if (cwc.stateQueue.Count > 0) {
					currentState = (State)cwc.stateQueue.Dequeue();
					currentTarget = currentState.getObj();
					if(currentState.getState() == "readyToOrder")
					{
						tablequeue.Enqueue(currentTarget);
					}
					walkToTarget();
					wc.setState(currentState.getState());
					return;
				}
			}else{
				wc.setState("WalkToBase");
			}
		}
	}
	
	public void startCustomerFollow() {
		if (currentTarget != null) {
			Vector3 n = currentTarget.transform.position;
			n.y += aboveTableBuffer;
			currentCC = (CustomerController)currentCustomer.GetComponent (typeof(CustomerController));
			currentCC.setTable (currentTarget);
			currentCC.walkTo (n, .25f);
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
