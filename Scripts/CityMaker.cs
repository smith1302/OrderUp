using UnityEngine;
using System.Collections;

public class CityMaker : MonoBehaviour {

	public Font myFont;
	public Texture2D myTexture;
	private Rect windowRect = new Rect(440, 25,500, 500);
	public int[] order;
	public int[] numbers;
	public Transform[] DatPlane;
	public Transform PlanesRoot;
	public LayerMask PlaneMask;
	public Texture2D[] allStructures;
	GameObject worldinfo;
	WorldVariables worldvar;
	int j;
	public Transform[] OldPlane;
	// Use this for initialization
	void Start () 
	{
		worldinfo = GameObject.FindGameObjectWithTag ("WorldInfo");
		worldvar = (WorldVariables)worldinfo.GetComponent (typeof(WorldVariables));

		if(worldvar.switched == false)
		{
		numbers = new int[60];
		order = new int[60];

		for(int i = 0; i < 60; i++)
		{
			numbers[i] = i;
		}
		DatPlane [j = Random.Range(0,60)].renderer.material.mainTexture = allStructures [1];
		order[0] = j;
		numbers [j] = -1;
		while(numbers[j] == -1)
		{
			j = Random.Range(0,60);
		}
		DatPlane [j].renderer.material.mainTexture = allStructures [2];
		order[1] = j;
		numbers [j] = -1;
		for(int i = 0;i < 40;i++)
		{
		while(numbers[j] == -1)
		{
			j = Random.Range(0,60);
		}
			order[i+2] = j;
			DatPlane [j].renderer.material.mainTexture = allStructures [3];
			
			numbers [j] = -1;
		}
		for(int i = 0;i < 18;i++)
		{
			while(numbers[j] == -1)
			{
				j = Random.Range(0,60);
			}
			order[42+i] = j;
			DatPlane [j].renderer.material.mainTexture = allStructures [0];
			numbers [j] = -1;
		}

		worldvar.setOrder (order);
			int r = 0;
		while(r < 60)
			{
				Debug.Log(order[r]);
				r++;
			}
			
		}
		else
		{
			order = worldvar.order;
			int z = order[0];
			Debug.Log("Switch: " + z);
			DatPlane[z].renderer.material.mainTexture = allStructures [1];
			z = order[1];
			Debug.Log("Switch: " + z);
			DatPlane[z].renderer.material.mainTexture = allStructures [2];
			for(int i = 0; i < 40; i++)
			{
				z = order[i+2];
				Debug.Log("Switch: " + z);
				DatPlane[z].renderer.material.mainTexture = allStructures [3];
			}
			for(int i = 0; i < 18; i++)
			{
				z = order[i+42];
				Debug.Log("Switch: " + z);
				DatPlane[z].renderer.material.mainTexture = allStructures[0];
			}
		}
	}

	// Update is called once per frame
	void Update () 
	{

		
	}


}