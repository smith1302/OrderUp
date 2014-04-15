using UnityEngine;
using System.Collections;

public class CustomerController : MonoBehaviour {
	WalkingController wc;
	CustomerWaiterController cwc;
	GameObject orderUp;
	GameObject table;
	GameObject waiter;
	GameObject instantiatedorderUp;
	Properties p;
	float timer;
	float timeToOrder;
	public GameObject moneyText;
	public bool hasWaiter;
	// Use this for initialization
	void Start () {
		timeToOrder = 3;
		hasWaiter = false;
		wc = transform.GetComponent<WalkingController> ();
		cwc = (GameObject.FindGameObjectWithTag ("Script")).GetComponent<CustomerWaiterController> ();
		setState ("In Line");
		orderUp = (GameObject)Resources.Load ("order_up");

		Time.timeScale = 1f;
		timer = 0;
		walkNoWait(new Vector3((float)(((float)cwc.getNumInLine()-1)/2)+.25f,2.5f,0));
		cwc.customerQueue.Enqueue (gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		stateAssigner ();
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
				Destroy(gameObject);
				return;
			}
			if (Time.time >= timer && wc.state == "Eating") {
				walkNoWait(new Vector3(0,3f,0));
				p.setCustomer(null);
				cwc.addTableToQueue(table);
				setState("Leaving");
				Vector3 pos = transform.position;
				pos.z = -2f;
				pos.x = pos.x-.4f;
				GameObject money = (GameObject) Instantiate(moneyText, pos, Quaternion.identity);
				money.renderer.material.SetColor("_Color",Color.green);
				//moveMoneyUp(money);
				return;
			}
			//this state will be set by the waiter when we comes by
			if (wc.state == "initEating") {
				timer = Time.time + 3; //how long will they eat for?
				setState("Eating");
				return;
			}
			if (wc.state =="waitForFood") {
				DestroyObject(instantiatedorderUp);
				setState("WaitAfterFood");
				return;
			}

			if (Time.time >= timer && wc.state == "Order") {
				setState("WaitAfterOrder");
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

	/*void moveMoneyUp(GameObject money) {
		money.transform.position = new Vector3 (transform.position.x, transform.position.y+., transform.position.z);
	}*/

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
		}
	}

	public void setState(string s) {
		wc.setState (s);
	}

	public string getState() {
		return wc.getState ();
	}
}
