using UnityEngine;
using System.Collections;

public class ManagerCustomerController : MonoBehaviour {
	WalkingController wc;
	ManagerCustomerWaiterController cwc;
	ManagerMinigame mm;
	GameObject orderUp;
	GameObject table;
	GameObject instantiatedorderUp;
	GameObject instantiatedAnger;
	GameObject angry;
	GameObject waiter;
	Properties p;
	float timer;
	float timer2;
	float timeToOrder;
	double anger;
	bool angertrue;
	bool onesec;
	int wait;
	int x;
	bool dequeue;
	public bool hasWaiter;
	GameObject[] customerArray;
	bool wasinline; 
	// Use this for initialization
	void Start () {
		wasinline = false;
		customerArray = new GameObject[20];
		hasWaiter = false;
		wait = 0;
		timer2 = 0;
		onesec = false;
		int x = 1;
		timeToOrder = 3;
		anger = Random.Range(-100, 70);
		angertrue = false;
		wc = transform.GetComponent<WalkingController> ();
		cwc = (GameObject.FindGameObjectWithTag ("Script")).GetComponent<ManagerCustomerWaiterController> ();
		setState ("In Line");
		orderUp = (GameObject)Resources.Load ("order_up");
		angry = (GameObject)Resources.Load ("Anger");
		
		mm = (GameObject.FindGameObjectWithTag ("MainCamera")).GetComponent<ManagerMinigame> ();
		Time.timeScale = 1f;
		timer = 0;
		walkNoWait(new Vector3((float)(((float)cwc.getNumInLine()-1)/2)+.25f,2.5f,0));
		cwc.customerQueue.Enqueue (gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		if(cwc.Begin)
		{
		stateAssigner ();
		
		int headsortails = Random.Range (0, 2);
		if (anger < 150 && (wc.state == "Wait" || wc.state == "In Line") && headsortails == 1 ) 
		{
			anger += .2;
		}
		
		if(anger > 150)
		{
			anger = 150;
		}
		
		if(anger >= 100 && angertrue == false)
		{
			generateAnger(transform.position);
			angertrue = true;
			
		}
		if(anger < 100 && angertrue == true)
		{
			Destroy(instantiatedAnger);
			angertrue = false;
		}
		if(angertrue)
		{
			Vector3 myposition = transform.position;
			myposition.y += .7f;
			instantiatedAnger.transform.position = myposition;
			timer2 += Time.deltaTime;
			if (timer2 > x-.1 && timer2 < x+.1)
			{
				onesec = true;
				x += 1;
			}
			if(onesec)
			{
				wait += 1;
				onesec = false;
			}
			if(wait == 7)
			{
				if(wc.getState() == "In Line")
				{
						wasinline = true;
						dequeue = true;
				}
				timer = Time.time + 1;
				setState("Eating");
			}
		}
		}
		
	}

	public void setWaiter(GameObject w) {
		waiter = w;
		hasWaiter = true;
	}
		
	public void setTable(GameObject t) {
		table = t;
		p = (Properties) table.GetComponent(typeof(Properties));
		p.setCustomer (gameObject);
	}
	
	void stateAssigner() {
		//if we finished last task
		if (!wc.hasTarget) {
			if (wc.state == "Leaving") {
				if(anger >= 100)
				{
					mm.unhappy();


					if(wasinline)
					{
					cwc.shiftWaitingCustomers();
					wasinline = false;
					}
				}

				if(p != null && wasinline == false)
				{
					p.setCustomer(null);
					cwc.addTableToQueue(table);
				}

				Destroy(gameObject);
				Destroy(instantiatedAnger);

				return;
			}
			if (Time.time >= timer && wc.state == "Eating") {
				walkNoWait(new Vector3(0,3f,0));
	
				if(instantiatedorderUp != null)
				{
					DestroyObject(instantiatedorderUp);
				}
				if(dequeue == true)
				{
					cwc.customerQueue.Dequeue();
					dequeue = false;
				}
				setState("Leaving");
				return;
			}
			//this state will be set by the waiter when we comes by
			if (wc.state == "initEating") {
				timer = Time.time + 3; //how long will they eat for?
				setState("Eating");
				return;
			}
			if (wc.state =="waitForFood") {
				if(anger >= 35)
				{
					anger += -35;
				}
				else if(anger < 35)
				{
					anger += -anger;
				}
				DestroyObject(instantiatedorderUp);
				setState("Wait");
				return;
			}
			
			if (Time.time >= timer && wc.state == "Order") {
				setState("Wait");
				if (table != null) {
					generateReady(table.transform.position);
				}
				return;
			}
			//we are seated
			if (wc.state == "seatedAtTable") {
				timer = Time.time + timeToOrder; //how long it takes to order
				setState("Order");
				return;
			}
		}
	}
	
	public void generateAnger(Vector3 v) {
		v.y += 1f;
		v.z = -1f;
		instantiatedAnger = (GameObject)Instantiate(angry, v, Quaternion.identity);
		
	}
	
	public void reduceAnger(int x)
	{
		anger += -x;
	}
	
	
	void generateReady(Vector3 v) {
		v.y += 1f;
		v.z = -1f;
		instantiatedorderUp = (GameObject)Instantiate(orderUp, v, Quaternion.identity);
		State s = new State (table, "readyToOrder");
		s.setOptionalObj (gameObject);
		cwc.stateQueue.Enqueue (s);
		
	}
	
	public void walkNoWait(Vector3 v) {
		wc.goalCoord = v;
		wc.startWalk ();
		
		if (wc.state == "walkToTable") {
			//set next task
			setState("seatedAtTable");
		}
	}
	
	public void walkTo(Vector3 v, float time) {
		StartCoroutine(waitBeforeWalk(time,v));
	}
	
	IEnumerator waitBeforeWalk(float num, Vector3 v) {
		yield return new WaitForSeconds(num);
		wc.goalCoord = v;
		wc.startWalk ();
		if (wc.state == "walkToTable") {
			//set next task
			setState("seatedAtTable");
			if(anger >= 60)
			{
				anger += -60;
			}
			else if(anger < 60)
			{
				anger += -anger;
			}
		}
	}


	public void setState(string s) {
		wc.setState (s);
	}
	
	public string getState() {
		return wc.getState ();
	}






}

