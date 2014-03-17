using UnityEngine;
using System;

public class State
{
		private string state;
		private GameObject obj;
		private GameObject optionalObj;

		public State (GameObject o, string s)
		{
			state = s;
			obj = o;
		}

		public void setOptionalObj(GameObject obj) {
			optionalObj = obj;
		}
	
		public string getState() {
			return state;
		}

		public GameObject getOptionalObj() {
			return optionalObj;
		}
	
		public GameObject getObj() {
			return obj;
		}
}

