using UnityEngine;
using System.Collections;

public class Employees : MonoBehaviour {
	string[] firstname;
	string[] lastname;
	public string[] waitername;
	string[] chefname;
	string[] managername;
	public GameObject[] waiters;
	GameObject[] chefs;
	GameObject[] managers;
	string name;

	// Use this for initialization
	void Start () {
		firstname = new string[10];
		lastname = new string[10];
		firstname [0] = "Jacob";
		firstname [1] = "Mason";
		firstname [2] = "Jesse";
		firstname [3] = "Elijah";
		firstname [4] = "William";
		firstname [5] = "Noah";
		firstname [6] = "Daniel";
		firstname [7] = "Ethan";
		firstname [8] = "David";
		firstname [9] = "Andrew";

		lastname [0] = "Smith";
		lastname [1] = "Johnson";
		lastname [2] = "Williams";
		lastname [3] = "Jones";
		lastname [4] = "Brown";
		lastname [5] = "Davis";
		lastname [6] = "Miller";
		lastname [7] = "Wilson";
		lastname [8] = "White";
		lastname [9] = "Anderson";

		waiters = new GameObject[10];
		chefs = new GameObject[10];
		managers = new GameObject[5];
		waitername = new string[10];
		chefname = new string[10];
		managername = new string[10];
	}

	public string createName()
	{
		name = firstname [Random.Range (0, 10)] + " " + lastname [Random.Range (0, 10)];
		return name;
	}

	public int setwaiterArray(GameObject waiter)
	{
		for(int i =0; i < 10; i++)
		{
			if(waiters[i] == null)
			{
				waiters[i] = waiter;
				return i;
			}
		}
		return -1;
	}

	public string[] getwaiternameArray()
	{
		return waitername;
	}

	public GameObject[] getwaiterArray()
	{
		return waiters;
	}

	public void setwaiternameArray(int index, string name)
	{
		waitername [index] = name;
	}
	// Update is called once per frame
	void Update () {
	
	}
}
