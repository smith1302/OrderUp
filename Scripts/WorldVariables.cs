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
	public bool switched;
	float elapsed;
	public int money;
	int numofwaiters;
	int numofchefs;
	int x;
	int income;
	bool restaurant;
	int waitercost;
	


	// Use this for initialization
	void Start () 
	{
		waitercost = 100;
		restaurant = true;
		switched = false;
		numofcustomers = 0;
		numofrestaurants = 0;
		numofAIrestaurants = 0;
		AImonthlyincome = 0;
		AIemployeenumber = 0;
		AIpopularity = 0;
		numofwaiters = 0;
		numofchefs = 0;
		x = 0;
		income = 500;
		order = new int[60];
		elapsed = Time.time;
		money = 10;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(!restaurant)
		{
			if (Time.time - elapsed >= 3)
			{
			elapsed = Time.time;
			generateMoney();
			}
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


	public void switchtoRestaurant()
	{
		restaurant = !restaurant;
	}

	void generateMoney() {
		numofcustomers++;
		setIncome (money);
		}
	public void setMoney(float percent)
	{
		money += (int)((float)money * percent);
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
