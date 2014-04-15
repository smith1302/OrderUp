using UnityEngine;
using System.Collections;

public class ManagerController : MonoBehaviour {
	
	GameObject currentTarget;
	WalkingController wc;
	ManagerCustomerController cc;
	ManagerWaiterController wcc;
	Vector3 wordPos;
	ManagerCustomerWaiterController cwc;

	// Use this for initialization
	void Start () {
		wc = transform.GetComponent<WalkingController> ();	
		currentTarget = null;
		wordPos = new Vector3(0,0,0);
		cwc = (GameObject.FindGameObjectWithTag ("Script")).GetComponent<ManagerCustomerWaiterController> ();

	}
	
	// Update is called once per frame
	void Update () {
		if(cwc.Begin)
		{
		Walker ();
		if(currentTarget != null)
		{
			interact ();
		}
		}
	}

	void walkTo(Vector3 v) {
		wc.goalCoord = v;
		wc.startWalk ();
	}

	void interact() {
		float addwordx = wordPos.x + .1f;
		float minuswordx = wordPos.x - .1f;
		float addwordy = wordPos.y + .1f;
		float minuswordy = wordPos.y - .1f;
		if (currentTarget.tag == "Customer" ) 
		{
		cc = (ManagerCustomerController)currentTarget.GetComponent (typeof(ManagerCustomerController));
			if((minuswordx < transform.position.x && addwordx > transform.position.x) && (minuswordy < transform.position.y && addwordy > transform.position.y))
			{
			cc.reduceAnger (100);
			currentTarget = null;
			}
		}
		else if (currentTarget.tag == "Waiter" )
		{
		wcc = (ManagerWaiterController)currentTarget.GetComponent (typeof(ManagerWaiterController));
			if((minuswordx < transform.position.x && addwordx > transform.position.x) && (minuswordy < transform.position.y && addwordy > transform.position.y))
			{
			wcc.reduceLazy (100);
			currentTarget = null;
			}
		}
		
	}

	void Walker() {
		Vector3 mousePos=new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f);
			if(Input.GetMouseButtonDown(0)) 
			{
			Ray ray = Camera.main.ScreenPointToRay(mousePos);
			RaycastHit hit;
			Debug.DrawRay(ray.origin, ray.direction*2000, Color.magenta, 10);
				if(Physics.Raycast(ray, out hit)) 
				{
				wordPos=hit.point;
				currentTarget = hit.transform.gameObject;

				} 
				else 
				{
				wordPos=Camera.main.ScreenToWorldPoint(mousePos);
				}
			wordPos.x += .5f;
			walkTo(wordPos);
			}
	}
	



}
