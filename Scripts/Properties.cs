using UnityEngine;
using System.Collections;

public class Properties : MonoBehaviour {
	public bool open;
	public string status;
	private GameObject customer;

	// Use this for initialization
	void Start () {
		open = true;
		status = "open";
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public GameObject getCustomer() {
		return customer;
	}

	public void setCustomer(GameObject c) {
		customer = c;
	}
}
