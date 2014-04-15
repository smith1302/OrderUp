using UnityEngine;
using System.Collections;

public class MinigameGUI : MonoBehaviour {
	
	public Font myFont;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnGUI ()
	{
		GUIStyle myStyle = new GUIStyle ();
		myStyle = new GUIStyle(GUI.skin.button);
		myStyle.font = myFont;
		if(GUI.Button (new Rect (130, 0, 140, 40), "Go back to restaurant", myStyle))
		{
			DontDestroyOnLoad (GameObject.FindGameObjectWithTag ("SceneInfo"));
			Application.LoadLevel("NormalRestaurant");
		}

	}
}
