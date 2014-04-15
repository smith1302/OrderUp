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


		waitername = new string[10];
		chefname = new string[10];
		managername = new string[5];
	}

	public string createName()
	{
		name = firstname [Random.Range (0, 10)] + " " + lastname [Random.Range (0, 10)];
		return name;
	}
	

	public string[] getwaiternameArray()
	{
		return waitername;
	}

	public string[] getchefnameArray()
	{
		return chefname;
	}

	public string[] getmanagernameArray()
	{
		return managername;
	}
	
	public GameObject[] getwaiterArray()
	{
		return waiters;
	}

	public int setwaiternameArray(string name)
	{
		for(int i =0; i < 10; i++)
		{
			if(waitername[i] == null)
			{
				waitername[i] = name;
				return i;
			}

		}
		return -1;
	}

	public int setchefnameArray(string name)
	{
		for(int i =0; i < 10; i++)
		{
			if(chefname[i] == null)
			{
				chefname[i] = name;
				return i;
			}
			
		}
		return -1;
	}

	public int setmanagernameArray(string name)
	{
		for(int i =0; i < 10; i++)
		{
			if(managername[i] == null)
			{
				managername[i] = name;
				return i;
			}
			
		}
		return -1;
	}

	// Update is called once per frame
	void Update () {
	
	}
}
