using UnityEngine;
using System.Collections;

public class MyScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(GameObject.FindGameObjectWithTag("Employees"));
		DontDestroyOnLoad(GameObject.FindGameObjectWithTag("WorldInfo"));
		DontDestroyOnLoad(GameObject.FindGameObjectWithTag("SceneInfo"));
		Application.LoadLevel ("NormalRestaurant");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
