using UnityEngine;
using System.Collections;

public class TheGUI : MonoBehaviour {
	public Font myFont;
	//public GUIStyle myStyle;
	private GameObject lastHitObj;
	public Texture2D myTexture;
	private Rect windowRect = new Rect(440, 25,500, 500);
	bool showObjectives = false;
	bool showRestaurant = false;
	bool showEmployees = false;
	bool showCompetition = false;
	bool showFinances = false;
	bool showNews = false;
	bool showAchievements = false;
	bool windowOpen = false;
	bool showBuying = false;
	int switchcase;
	public Texture2D[] allStructures;
	private Material OriginalMat;
	public Material HoverMat;
	GameObject worldinfo;
	GameObject myObject;
	WorldVariables worldvar;
	GameObject employeeinfo;
	Employees employeevar;
	CityMaker citymaker;
	GameObject camera;
	int restaurantcost;
	// Use this for initialization
	void Start () {
		int switchcase = 0;
		restaurantcost = 1000;
		employeeinfo = GameObject.FindGameObjectWithTag ("Employees");
		employeevar = (Employees)employeeinfo.GetComponent (typeof(Employees));
		worldinfo = GameObject.FindGameObjectWithTag ("WorldInfo");
		worldvar = (WorldVariables)worldinfo.GetComponent (typeof(WorldVariables));
		camera = GameObject.FindGameObjectWithTag ("MainCamera");
		citymaker = (CityMaker)camera.GetComponent (typeof(CityMaker));
	}
	
	// Update is called once per frame
	void Update () {
		if(worldvar.newAI)
		{

			for(int i = 0; i < 40; i++)
			{
				if(worldvar.newrestaurants[i] == 0 && citymaker != null)
				{
					worldvar.newrestaurants[i] = 2;
					int z = worldvar.order[i+2];
					citymaker.DatPlane[z].renderer.material.mainTexture = allStructures[2];
					worldvar.y--;
					break;
				}
			}

			if(worldvar.y == 0)
			{
				worldvar.newAI = false;
			}
		}
	
	}

	void WindowFunction1(int WindowID)
	{
		GUIStyle myStyle = new GUIStyle ();
		myStyle = new GUIStyle(GUI.skin.button);
		myStyle.font = myFont;
		GUI.TextArea (new Rect (100, 100, 300, 300), "It costs $" + restaurantcost+ " to buy a new restaurant");
		if (GUI.Button (new Rect (100, 450, 110, 40), "Buy", myStyle)) 
		{
			//if(worldvar.getIncome() >= restaurantcost)
			//{
			if(restaurantcost > 10000)
			{
				restaurantcost = 10000;
			}
			worldvar.setIncome(-restaurantcost);
			if(restaurantcost < 10000)
			{
				restaurantcost += 2000;
			}



			worldvar.setNumOfRestaurants();
			for(int i = 0; i < 40; i++)
			{
				if(worldvar.newrestaurants[i] == 0)
				{
				worldvar.newrestaurants[i] = 1;
				int z = worldvar.order[i+2];
				citymaker.DatPlane[z].renderer.material.mainTexture = allStructures[1];
				break;
				}
			}
			showBuying = false;
			windowOpen = false;
			//}
		}
		if (GUI.Button (new Rect (300, 450, 110, 40), "Close", myStyle)) 
		{
			showBuying = false;
			windowOpen = false;
		}
		GUI.DragWindow (new Rect (0, 0, 500, 500));
	}


	void WindowFunction(int WindowID){
		GUIStyle myStyle = new GUIStyle ();
		myStyle = new GUIStyle(GUI.skin.button);
		myStyle.font = myFont;
		if (GUI.Button (new Rect (200, 450, 110, 40), "Close", myStyle)) 
		{
			windowOpen = false;
		}
		GUI.DragWindow (new Rect (0, 0, 100, 100));

		if(showObjectives)
		{
			try
			{
				switch(switchcase)
				{
				case 0:
				{
				GUI.TextField(new Rect(100,100,300,300)," Have 1 waiter on payroll\n \n \n Have 1 chef on payroll\n \n \n Have 1 manager on payroll\n \n \nHave a monthly income of $1,000 \n \n \n Earn $5,000");
				break;
				}
				case 1:
				{
				GUI.TextField(new Rect(100,100,300,300)," Have 2 waiters on payroll\n \n \n Have 2 chefs on payroll\n \n \n Have 2 manager on payroll\n \n \nHave a monthly income of $3,000 \n \n \n Earn $10,000");
				break;
				}
				case 2:
				{
				GUI.TextField(new Rect(100,100,300,300)," Have 3 waiter on payroll\n \n \n Have 3 chef on payroll\n \n \n Have 3 manager on payroll\n \n \nHave a monthly income of $5,000 \n \n \n Earn $50,000");
				break;
				}
				case 3:
				{
				GUI.TextField(new Rect(100,100,300,300)," Have 4 waiter on payroll\n \n \n Have 4 chef on payroll\n \n \n Have 4 manager on payroll\n \n \nHave a monthly income of $10,000 \n \n \n Earn $100,000");
				break;
				}
				case 4:
				{
				GUI.TextField(new Rect(100,100,300,300)," Have 5 waiter on payroll\n \n \n Have 5 chef on payroll\n \n \n Have 5 manager on payroll\n \n \nHave a monthly income of $50,000 \n \n \n Earn $500,000");
				break;
				}
				default:
				{
				GUI.TextField(new Rect(100,100,300,300)," Have 1 waiter on payroll\n \n \n Have 1 chef on payroll\n \n \n Have 1 manager on payroll\n \n \nHave a monthly income of 1,000$ \n \n \n Earn $5,000$");
				break;
				}
			}
			}
			catch
			{
			}
		}

		if(showRestaurant)
		{
			GUI.TextField(new Rect(100,100,300,300)," Employees: " + (worldvar.getNumOfWaiters()+ worldvar.getNumOfChefs()) + "\n \n \nNumber of restaurants owned: " + worldvar.getNumOfRestaurants() +  "\n \n \nCustomers per month: " + (worldvar.getNumOfCustomers()));
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


			GUI.BeginScrollView(new Rect(100, 100, 500, 500), v ,new Rect(0, 0, 500, 500));
			GUI.TextArea(new Rect(0,0,300,300), waiter + chef + manager);
			GUI.EndScrollView();
		}
		if (showCompetition)
		{
			GUI.TextField(new Rect(100,100,300,300),"Number of restaurants owned: " + worldvar.getNumOfAiRestaurants() + "\n \n \nMonthly Income: " + worldvar.getAIMonthlyIncome() + "\n \n \nCustomers per month: " + worldvar.getAipopularity() );
		}
		if (showFinances) 
		{
			//GUI.TextField();
		}
		if(showAchievements)
		{
			GUI.TextField(new Rect(100,100,300,300)," Recruit your first employee \n \n Serve your first customer \n \n Own 2 restaurants \n \n Own 5 restaurants  \n \n Own 10 restaurants \n \n Make $10,000 \n \n Make $50,000 \n \n Make $100,000 \n \n Make $1,000,000 \n \n Become most popular restaurant chain");
		}
		if(showNews)
		{
			//GUI.TextField();
		}
	}

	void OnGUI () {
		GUIStyle myStyle = new GUIStyle ();
		GUIStyle myStyle1 = new GUIStyle ();
		myStyle = new GUIStyle(GUI.skin.button);
		myStyle.font = myFont;
		myStyle1 = new GUIStyle(GUI.skin.window);
		myStyle1.font = myFont;
		myStyle1.normal.background = myTexture;

		GUIStyle style = new GUIStyle(GUI.skin.label);
		style.font = myFont;
		style.fontSize = 35;
		style.normal.textColor = Color.white;
		GUI.Label (new Rect (10, 10, 150, 100), "$" + worldvar.getIncome(), style);

		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;

		/*if(showBuying != true)
		{
		if(Physics.Raycast(ray, out hit))
		{
			if(lastHitObj)
			{
				lastHitObj.renderer.material = OriginalMat;
			}
			lastHitObj = hit.collider.gameObject;
			OriginalMat = lastHitObj.renderer.material;
			Texture txt = lastHitObj.renderer.material.mainTexture;
			lastHitObj.renderer.material = HoverMat;
			lastHitObj.renderer.material.mainTexture = txt;
			if(Input.GetMouseButtonDown(0)) 
			{
				if(lastHitObj.renderer.material.mainTexture == allStructures [3])
				{
					myObject = lastHitObj;
					showBuying = true;
					windowOpen = true;
				}
			}
		}
		}

		else
		{
			if(lastHitObj)
			{
				lastHitObj.renderer.material = OriginalMat;
				lastHitObj = null;
			}
		}
		*/


		if (GUI.Button (new Rect (40, 540, 160, 40), "Buy another restaurant", myStyle)) 
		{
			showBuying = true;
			windowOpen = true;
			showObjectives = false;
			showRestaurant = false;
			showEmployees = false;
			showCompetition = false;
			showFinances = false;
			showNews = false;
			showAchievements = false;
		}

		if (showBuying == true && windowOpen == true) 
		{
			windowRect = GUI.Window(0, windowRect, WindowFunction1, "Buy new restaurant?");
		}


		if (GUI.Button (new Rect (220, 540, 110, 40), "Go to restaurant", myStyle)) 
		{
			DontDestroyOnLoad(GameObject.FindGameObjectWithTag("SceneInfo"));
			DontDestroyOnLoad(GameObject.FindGameObjectWithTag("WorldInfo"));
			worldvar.switchtoRestaurant();
			Application.LoadLevel("NormalRestaurant");
		}

		if (GUI.Button (new Rect (350, 540, 110, 40), "Objectives", myStyle)) 
		{
		   windowOpen = true;
		   showObjectives = true;
		   showBuying = false;
		   showRestaurant = false;
		   showEmployees = false;
		   showCompetition = false;
		   showFinances = false;
		   showNews = false;
		   showAchievements = false;
		}
		if (showObjectives == true && windowOpen == true) 
		{
			windowRect = GUI.Window(0, windowRect, WindowFunction, "Objectives", myStyle1);
		}

		if (GUI.Button (new Rect (480, 540, 115, 40), "My Restaurant", myStyle))
		{
			windowOpen = true;
			showRestaurant = true;
			showBuying = false;
			showObjectives = false;
			showEmployees = false;
			showCompetition = false;
			showFinances = false;
			showNews = false;
			showAchievements = false;
		}
		if (showRestaurant == true && windowOpen == true) 
		{
				windowRect = GUI.Window(0, windowRect, WindowFunction, "My Restarant", myStyle1);
		}
		if (GUI.Button (new Rect (610, 540, 115, 40), "Employees", myStyle))
		{
			windowOpen = true;
			showEmployees = true;
			showBuying = false;
			showRestaurant = false;
			showObjectives = false;
			showCompetition = false;
			showFinances = false;
			showNews = false;
			showAchievements = false;
		}
		if (showEmployees == true && windowOpen == true) 
		{
			windowRect = GUI.Window(0, windowRect, WindowFunction, "Employees", myStyle1);
		}
		if(GUI.Button (new Rect (740, 540, 115, 40), "Competitiors", myStyle))
		{
			windowOpen = true;
			showCompetition = true;
			showBuying = false;
			showRestaurant = false;
			showEmployees = false;
			showObjectives = false;
			showFinances = false;
			showNews = false;
			showAchievements = false;
		}
		if (showCompetition == true && windowOpen == true) 
		{
			windowRect = GUI.Window(0, windowRect, WindowFunction, "Competitiors", myStyle1);
		}
		if(GUI.Button (new Rect (870, 540, 115, 40), "Finances", myStyle))
		{
			windowOpen = true;
			showFinances = true;
			showBuying = false;
			showRestaurant = false;
			showEmployees = false;
			showCompetition = false;
			showObjectives = false;
			showNews = false;
			showAchievements = false;
		}
		if (showFinances == true && windowOpen == true) 
		{
			windowRect = GUI.Window(0, windowRect, WindowFunction, "Finances", myStyle1);
		}
		if(GUI.Button (new Rect (1000, 540, 115, 40), "News", myStyle))
		{
			windowOpen = true;
			showNews = true;
			showBuying = false;
			showRestaurant = false;
			showEmployees = false;
			showCompetition = false;
			showFinances = false;
			showObjectives = false;
			showAchievements = false;
		}
		if (showNews == true && windowOpen == true) 
		{
			windowRect = GUI.Window(0, windowRect, WindowFunction, "Recent News", myStyle1);
		}
		if(GUI.Button (new Rect (1130, 540, 115, 40), "Achievements", myStyle))
		{
			windowOpen = true;
			showAchievements = true;
			showBuying = false;
			showRestaurant = false;
			showEmployees = false;
			showCompetition = false;
			showFinances = false;
			showNews = false;
			showObjectives = false;
		}
		if (showAchievements == true && windowOpen == true) 
		{
			windowRect = GUI.Window(0, windowRect, WindowFunction, "Achievements", myStyle1);
		}



	}
}
