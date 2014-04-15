using UnityEngine;
using System.Collections;

public class ChefController : MonoBehaviour {
	WalkingController wc;
	CustomerWaiterController cwc;
	private State currentState;
	string myState;
	WaiterController wac;
	public bool orderready;
	public bool setonce;
	GameObject worldinfo;
	WorldVariables worldvar;
	GameObject employeeinfo;
	Employees employeevar;
	GameObject restaurantinfo;
	RestaurantGUI restaurantvar;
	int index;
	int x;


	// Use this for initialization
	void Start () {
		restaurantinfo = GameObject.FindGameObjectWithTag ("MainCamera");
		restaurantvar = (RestaurantGUI)restaurantinfo.GetComponent (typeof(RestaurantGUI));
		wc = transform.GetComponent<WalkingController> ();
		cwc = (GameObject.FindGameObjectWithTag ("Script")).GetComponent<CustomerWaiterController> ();
		worldinfo = GameObject.FindGameObjectWithTag ("WorldInfo");
		worldvar = (WorldVariables)worldinfo.GetComponent (typeof(WorldVariables));
		orderready = false;
		setonce = false;
	
	}
	
	// Update is called once per frame
	void Update () {
		stateAssigner ();
		if(worldvar.getNumOfChefs() == 1)
		{
			x = 10;
		}
		if(worldvar.getNumOfChefs() == 2)
		{
			x =8;
		}
		if(worldvar.getNumOfChefs() == 3)
		{
			x = 6;
		}
		if(worldvar.getNumOfChefs() == 4)
		{
			x = 4;
		}
		if(worldvar.getNumOfChefs() == 5)
		{
			x = 2;
		}
	}



	void stateAssigner() {
		if (myState == "GotOrder" && setonce == false && worldvar.getNumOfChefs() != 0) 
			{
			setonce = true;
			Invoke("setOrder",x);
			}

	}

	void setOrder()
	{
		orderready = true;
	}

	public void setState(string s) {
		myState = s;
	}
	
	public string getState() {
		return myState ;
	}

}



