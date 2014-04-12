using UnityEngine;
using System.Collections;

public class RestaurantGUI : MonoBehaviour {

	public Font myFont;
	GameObject worldinfo;
	WorldVariables worldvar;
	public Vector3 waiterVector;
	public GameObject waiterObj;
	public bool newwaiter;
	// Use this for initialization
	void Start () {
		worldinfo = GameObject.FindGameObjectWithTag ("WorldInfo");
		if(worldinfo != null)
		worldvar = (WorldVariables)worldinfo.GetComponent (typeof(WorldVariables));
		waiterVector = new Vector3(0, -3f, 0);
		waiterObj = (GameObject)Resources.Load ("Waiter");
		newwaiter = false;
	}
	
	// Update is called once per frame
	void Update () {
	
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
		if(GUI.Button (new Rect (130, 100, 140, 40), "Add a waiter", myStyle))
		{
			//if(worldvar.getIncome() >= worldvar.getWaiterCost())
			//{
			worldvar.setIncome(-worldvar.getWaiterCost());
			worldvar.setWaiterCost(200);
			worldvar.setNumOfWaiters();
			newwaiter = true;
			Vector3 v = waiterVector;
			v.y += Random.Range (0, 4);
			v.x += Random.Range (-1, 1);
			Instantiate(waiterObj, v, Quaternion.identity);
			//}
		}
	}
}
