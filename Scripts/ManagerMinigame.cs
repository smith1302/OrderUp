using UnityEngine;
using System.Collections;


public class ManagerMinigame : MonoBehaviour {
	public Font myFont;
	float timer;
	int x;
	bool onesec;
	bool sixtysec;
	string gametimer;
	int minute = 2;
	int sec = 0;
	bool showObjective;
	bool lose;
	bool win;
	private Rect windowRect = new Rect(440, 25,500, 500);
	private Rect windowRect1 = new Rect(530, 130,300, 300);
	ManagerCustomerWaiterController cwc;
	ManagerCustomerController cc;
	GameObject worldinfo;
	WorldVariables worldvar;
	int unhappypeeps;

	// Use this for initialization
	void Start () {
		worldinfo = GameObject.FindGameObjectWithTag ("WorldInfo");
		worldvar = (WorldVariables)worldinfo.GetComponent (typeof(WorldVariables));
		unhappypeeps = 0;
		onesec = false;
		sixtysec = false;
		timer = 0;
		x = 1;
		gametimer = string.Format("{0:D1}:{1:D2}", minute, sec);
		showObjective = true;
		win = false;
		lose = false;
		cwc = (GameObject.FindGameObjectWithTag ("Script")).GetComponent<ManagerCustomerWaiterController> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(cwc.Begin)
		{
		timer += Time.deltaTime;
		if(minute == 2 && sec == 0)
		{
			minute = 1;
			sec = 59;
		}
		if (timer > 59.9 && timer < 60.1)
		{
			sixtysec = true;
			x = 0;
			timer = 0;
			sec = 60;
		}
		if (timer > x-.1 && timer < x+.1)
		{
			onesec = true;
			x += 1;
		}
		if(sixtysec)
		{
			minute += -1;
			sixtysec = false;
		}
		if(onesec)
		{
			sec += -1;
			onesec = false;
		}

		if(minute == 0 && sec == 0)
		{
			/*win = true;
			
			cwc.Begin = false;*/
		}
			gametimer = string.Format("{0:D1}:{1:D2}", minute, sec);
		
		if(unhappypeeps == 5)
		{
				/*lose = true;
				cwc.Begin = false;*/
		}

		}

	}

	public void unhappy()
	{
		unhappypeeps += 1;
	}

	void WindowFunction(int WindowID){
		GUIStyle myStyle = new GUIStyle ();
		myStyle = new GUIStyle(GUI.skin.button);
		myStyle.font = myFont;
		GUI.TextArea (new Rect (100, 100, 300, 300), "Keep your customers happy!\n\n\n " +
		    "For 2 minutes don't let 5 customers leave angry  \n\n\nClick on lazy waiters to discpline them" +
			"\n\n\nClick on angry customers to increase their happiness\n\n\n\n\n\n\n" +
			"Reward: Level up managers (+20% income)");
		if (GUI.Button (new Rect (200, 450, 110, 40), "I'm ready", myStyle)) 
		{
			cwc.Begin = true;
			showObjective = false;
		}
		GUI.DragWindow (new Rect (0, 0, 500, 500));
	}

	void WindowFunction1(int WindowID){
		GUIStyle myStyle = new GUIStyle ();
		myStyle = new GUIStyle(GUI.skin.button);
		myStyle.font = myFont;
		if (GUI.Button (new Rect (90, 50, 110, 40), "Try Again", myStyle)) 
		{
			Application.LoadLevel("ManagerMinigame");
		}
		if (GUI.Button (new Rect (80, 150, 130, 40), "Back to restaurant", myStyle)) 
		{
			Application.LoadLevel("NormalRestaurant");
		}
		GUI.DragWindow (new Rect (0, 0, 300, 300));
	}

	void WindowFunction2(int WindowID){
		GUIStyle myStyle = new GUIStyle ();
		myStyle = new GUIStyle(GUI.skin.button);
		myStyle.font = myFont;

		if (GUI.Button (new Rect (80, 100, 130, 40), "Back to restaurant", myStyle)) 
		{
			worldvar.setMoney(.2f);
			Application.LoadLevel("NormalRestaurant");
		}
		GUI.DragWindow (new Rect (0, 0, 300, 300));
	}

	void OnGUI () 
	{
		GUIStyle style = new GUIStyle(GUI.skin.label);
		style.font = myFont;
		style.fontSize = 35;
		style.normal.textColor = Color.black;
		GUI.Label (new Rect (890, 10, 150, 100),(""+gametimer), style);
		GUIStyle style1 = new GUIStyle (GUI.skin.window);
		style1.font = myFont;
		style.fontSize = 20;


		if(showObjective == true)
		{
			windowRect = GUI.Window(0, windowRect, WindowFunction, "Manager Minigame", style1);
		}

		if(lose == true)
		{
			windowRect = GUI.Window(0, windowRect1, WindowFunction1, "You did not complete the challenge",style1);
		}
		if(win == true)
		{
			windowRect = GUI.Window(0, windowRect1, WindowFunction2, "You completed the challenge!!!",style1);
		}
	}
}
