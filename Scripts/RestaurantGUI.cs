using UnityEngine;
using System.Collections;

public class RestaurantGUI : MonoBehaviour {
	
	public Font myFont;
	GameObject worldinfo;
	WorldVariables worldvar;
	public Vector3 waiterVector;
	public GameObject waiterObj;
	public bool newwaiter;
	public bool newchef;
	bool buywaiter;
	bool buychef;
	bool buymanager;
	bool showEmployees;
	bool windowOpen;
	GameObject employeeinfo;
	Employees employeevar;
	int index;
	CustomerWaiterController cwc;
	private Rect windowRect = new Rect(440, 25,500, 500);
	Vector3 v;
	
	
	// Use this for initialization
	void Start () {
		cwc = (GameObject.FindGameObjectWithTag ("Script")).GetComponent<CustomerWaiterController> ();
		employeeinfo = GameObject.FindGameObjectWithTag ("Employees");
		employeevar = (Employees)employeeinfo.GetComponent (typeof(Employees));
		worldinfo = GameObject.FindGameObjectWithTag ("WorldInfo");
		if(worldinfo != null)
			worldvar = (WorldVariables)worldinfo.GetComponent (typeof(WorldVariables));
		waiterVector = new Vector3(0, -3f, 0);
		waiterObj = (GameObject)Resources.Load ("Waiter");
		newwaiter = false;
		showEmployees = false;
		windowOpen = false;
		buychef = false;
		buymanager = false;
		buywaiter = false;
		v = new Vector3 ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void WindowFunction(int WindowID){
		GUIStyle myStyle = new GUIStyle ();
		myStyle = new GUIStyle(GUI.skin.button);
		myStyle.font = myFont;
		if (GUI.Button (new Rect (200, 450, 110, 40), "Close", myStyle)) 
		{
			windowOpen = false;
		}
		
		if (showEmployees) 
		{
			Vector2 v = new Vector2(0,100);
			string[] waiters = employeevar.getwaiternameArray();
			string waiter = "";
			waiter = "Waiters: \n";
			for(int i = 0; i < 10; i++)
			{
				if(waiters[i] != null)
				{
					waiter += waiters[i] + "\n";
				}
			}
			
			string[] chefs = employeevar.getchefnameArray();
			string chef = "";
			chef = "\nChefs: \n";
			for(int i = 0; i < 10; i++)
			{
				if(chefs[i] != null)
				{
					chef += chefs[i] + "\n";
				}
			}
			
			string[] managers = employeevar.getmanagernameArray();
			string manager = "";
			manager = "\nManagers: \n";
			for(int i = 0; i < 5; i++)
			{
				if(managers[i] != null)
				{
					manager += managers[i] + "\n";
				}
			}
			
			
			GUI.BeginScrollView(new Rect(100, 50, 500, 500), v ,new Rect(0, 0, 500, 500));
			GUI.TextArea(new Rect(0,0,300,300), waiter + chef + manager);
			GUI.EndScrollView();
			
			if(GUI.Button(new Rect(30,370, 110, 40), "Buy Waiter"))
			{
				buywaiter = true;
			}
			if(GUI.Button(new Rect(190,370, 110, 40), "Buy Chef"))
			{
				buychef = true;
			}
			
			if(GUI.Button(new Rect(350,370, 110, 40), "Buy Manager"))
			{
				buymanager = true;
			}

			GUI.Label(new Rect(30,420, 110, 40),  "$"+worldvar.getWaiterCost());
			GUI.Label(new Rect(190,420, 110, 40), "$"+worldvar.getChefCost());
			GUI.Label(new Rect(350,420, 110, 40), "$"+worldvar.getManagerCost());
				
			if(buywaiter)
			{
				//if(worldvar.getIncome() >= worldvar.getWaiterCost() && worldvar.getNumOfWaiters < 10)
				//{
				worldvar.setIncome(-worldvar.getWaiterCost());
				worldvar.setWaiterCost(200);
				worldvar.setNumOfWaiters();
				newwaiter = true;
				v = waiterVector;
				v.y += Random.Range (0, 4);
				v.x += Random.Range (-1, 1);
				Instantiate(waiterObj, v, Quaternion.identity);
				buywaiter = false;
				//}
			}
			
			if(buychef)
			{
				
				//if(worldvar.getIncome() >= worldvar.getChefCost() && worldvar.getNumOfChefs() < 10)
				//{
				worldvar.setIncome(-worldvar.getChefCost());
				worldvar.setChefCost(200);
				worldvar.setNumOfChefs();
				name = employeevar.createName ();
				index = employeevar.setchefnameArray (name);
				buychef = false;
				//}
				
			}
			
			if(buymanager)
			{
				//if(worldvar.getIncome() >= worldvar.getManagerCost() && worldvar.getNumOfManagers() < 5)
				//{
				worldvar.setIncome(-worldvar.getManagerCost());
				worldvar.setManagerCost(500);
				worldvar.setNumOfManagers();
				name = employeevar.createName ();
				index = employeevar.setmanagernameArray (name);
				cwc.setz();
				buymanager = false;
				//}	
			}
			
		}
	}
	
	void OnGUI ()
	{
		GUIStyle myStyle = new GUIStyle ();
		myStyle = new GUIStyle(GUI.skin.button);
		myStyle.font = myFont;
		GUIStyle style = new GUIStyle(GUI.skin.label);
		style.font = myFont;
		style.fontSize = 35;
		style.normal.textColor = Color.black;
		GUI.Label (new Rect (890, 10, 150, 100), "$" + worldvar.getIncome(), style);
		if(GUI.Button (new Rect (130, 0, 140, 40), "Go to city view", myStyle))
		{
			
			worldvar.switchtoRestaurant();
			DontDestroyOnLoad (GameObject.FindGameObjectWithTag ("WorldInfo"));
			DontDestroyOnLoad (GameObject.FindGameObjectWithTag ("SceneInfo"));
			Application.LoadLevel("WorldScene");
		}
		if(GUI.Button (new Rect (130, 50, 140, 40), "Manager Minigame", myStyle))
		{
			Application.LoadLevel("ManagerMiniGame");
		}
		if(GUI.Button (new Rect (130, 100, 140, 40), "Employees", myStyle))
		{
			showEmployees = true;
			windowOpen = true;
		}
		
		if(showEmployees == true && windowOpen == true) 
		{
			windowRect = GUI.Window(0, windowRect, WindowFunction, "Employees");
		}
		
		
	}
}
