using UnityEngine;
using System.Collections;


public class WorldVariables : MonoBehaviour {

	 int numofcustomers;
	public int numofrestaurants;
	public int numofAIrestaurants;
	public int AImonthlyincome;
	public int AIemployeenumber;
	public int AIpopularity;
	public int[] order;
	public int[] newrestaurants;
	public bool switched;
	float elapsed;
	public int money;
	public bool newAI;
	public int y;
	int numofmanagers;
	int numofwaiters;
	int numofchefs;
	int x;
	int start;
	int income;
	int numofmonths;
	bool restaurant;
	int waitercost;
	int chefcost;
	int managercost;
	float timer;
	bool month;



	// Use this for initialization
	void Start () 
	{
		newAI = false;
		y = 0;
		start = 2;
		numofmonths = 0;
		chefcost = 100;
		waitercost = 100;
		managercost = 500;
		restaurant = true;
		switched = false;
		numofcustomers = 0;
		numofrestaurants = 1;
		numofAIrestaurants = 0;
		AImonthlyincome = 0;
		AIemployeenumber = 0;
		AIpopularity = 0;
		numofwaiters = 0;
		numofchefs = 0;
		x = 0;
		income = 500;
		order = new int[60];
		newrestaurants = new int[40];
		elapsed = Time.time;
		month = false;
		money = 10;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(!restaurant && numofwaiters > 0 && numofchefs > 0)
		{
			if (Time.time - elapsed >= 4)
			{
			elapsed = Time.time;
			generateMoney();
			}
		}

		timer += Time.deltaTime;

		if (timer > 119.9 && timer < 120.1)
		{
			timer = 0;
			numofcustomers = 0;
			month = true;
		}

		if(month)
		{
			numofmonths++;
			month = false;
		}

		if(numofmonths == 8)
		{
			numofAIrestaurants++;
			numofmonths = 0;
			newAI = true;
			y++;
			Debug.Log("Y = " + y);
		}

		
	}
	
	public int getWaiterCost()
	{
		return waitercost;
	}

	public void setWaiterCost(int cost)
	{
		waitercost += cost;
	}

	public int getChefCost()
	{
		return chefcost;
	}
	
	public void setChefCost(int cost)
	{
		chefcost += cost;
	}

	public int getManagerCost()
	{
		return managercost;
	}
	
	public void setManagerCost(int cost)
	{
		managercost += cost;
	}

	

	public void switchtoRestaurant()
	{
		restaurant = !restaurant;
	}

	void generateMoney() {
		numofcustomers++;
		setIncome (money*getNumOfRestaurants());
		}
	public void setMoney(float percent)
	{
		money += (int)((float)money * percent);
		money = money * getNumOfRestaurants ();
		Debug.Log ("Money : "+money);
		Debug.Log ("Income : "+income);
	}

	public void setOrder(int[] order)
	{
		this.order = order;
		switched = true;
	}

	public int[] getOrder()
	{
		return order;
	}

	public void setNumOfRestaurants()
	{
		numofrestaurants++;
	}

	public int getNumOfRestaurants()
	{
		return numofrestaurants;
	}
	public int getNumOfAiRestaurants()
	{
		return numofAIrestaurants;
	}
	public int getAIMonthlyIncome ()
	{
		return AImonthlyincome;
	}
	public int getAiEmployeeNumber()
	{
		return AIemployeenumber;
	}
	public int getAipopularity()
	{
		return AIpopularity;
	}

	public void setIncome()
	{
		income += money;
		
	}

	public void setIncome(int money)
	{
		income += money;
		
	}
	
	public int getIncome()
	{
		return income;
	}
	
	public void switches()
	{
		x++;
	}
	
	public void setNumOfCustomers()
	{
		numofcustomers++;
	}
	
	public void setNumOfWaiters()
	{
		numofwaiters++;
	}
	
	public void setNumOfChefs()
	{
		numofchefs++;
	}

	public void setNumOfManagers()
	{
		numofmanagers++;
	}

	public int getNumOfManagers()
	{
		return numofmanagers;
	}
	
	public int getNumOfCustomers()
	{
		return numofcustomers;
	}
	
	public int getNumOfWaiters()
	{

		return numofwaiters;
	}
	
	public int getNumOfChefs()
	{
		return numofchefs;
	}

}
