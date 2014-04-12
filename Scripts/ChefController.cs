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


	// Use this for initialization
	void Start () {
		wc = transform.GetComponent<WalkingController> ();
		cwc = (GameObject.FindGameObjectWithTag ("Script")).GetComponent<CustomerWaiterController> ();
		orderready = false;
		setonce = false;
	
	}
	
	// Update is called once per frame
	void Update () {
		stateAssigner ();
	}



	void stateAssigner() {
		if (myState == "GotOrder" && setonce == false) 
			{
			setonce = true;
			Invoke("setOrder",5);
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



