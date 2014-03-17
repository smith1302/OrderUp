using UnityEngine;
using System.Collections;
using Pathfinding;

public class WalkingController : MonoBehaviour {

	private Animator anim;
	private string currentState;
	private Sprite sprite;
	private float speed = 1.8f;
	private Vector3 playerCoord;
	public Vector3 goalCoord;
	private Vector3 wpCoord;
	public Seeker seeker;
	private Path path;
	public int waypoint;
	public bool hasTarget;
	public string state;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		seeker = GetComponent<Seeker>();

		currentState = "guy1_idle_down";
		playerCoord = Camera.main.WorldToScreenPoint (transform.position);
		waypoint = 0;
		hasTarget = false;

		setAnim (currentState);
	}
	
	// Update is called once per frame
	void Update () {
		walkToCoord ();
	}

	public void setState(string state) {
		this.state = state;
	}

	public void startWalk() {
		seeker.StartPath (transform.position, goalCoord, OnPathComplete);
		waypoint = 0;
		hasTarget = true;
	}

	void walkToCoord() {
		if (path == null || !hasTarget) {
			return;
		}
		if (waypoint >= path.vectorPath.Count) {
			walk ("idle");
			path = null;
			hasTarget = false;
			return;
		}
		playerCoord = transform.position;
		Vector3 dir = (wpCoord-playerCoord).normalized;
		dir *= speed * Time.fixedDeltaTime;
		transform.position = new Vector2 (playerCoord.x + dir.x, playerCoord.y + dir.y);
		playerCoord = transform.position;
		if (Mathf.Abs (dir.y) > Mathf.Abs (dir.x * 1.5f)) {
				if (dir.y > 0) {
						walk ("up");
				} else if (dir.y <= 0) {
						walk ("down");
				}
		} else {
				if (dir.x > 0) {
						walk ("right");
				} else if (dir.x <= 0) {
						walk ("left");
				}
		}
		if (Vector3.Distance (playerCoord,wpCoord) < .02) {
			waypoint++;
			if (waypoint < path.vectorPath.Count) {
				wpCoord = path.vectorPath[waypoint];
			}
			return;
		}
	}

	public void OnPathComplete (Path p) {
		if (p.error) {
			//Nooo, a valid path couldn't be found
			Debug.Log("Error: "+p.error);
		} else {
			path = p;
			wpCoord = path.vectorPath[0];
			waypoint = 0;
		}
	}

	void setAnim(string name) {
		currentState = name;
		anim.CrossFade(currentState, 0f);
	}

	void walk(string direction) {
		if (direction == "up") {
				setAnim ("guy1_up");
				//transform.position = new Vector2 (transform.position.x, transform.position.y + speed);
		} else if (direction == "down") {
				setAnim ("guy1_down");
				//transform.position = new Vector2 (transform.position.x, transform.position.y - speed);
		} else if (direction == "left") {
				setAnim ("guy1_left");
				//transform.position = new Vector2 (transform.position.x - speed, transform.position.y);
		} else if (direction == "right") {
				setAnim ("guy1_right");
				//transform.position = new Vector2 (transform.position.x + speed, transform.position.y);
		} else if (direction == "idle") {
			switch (currentState) {
				case "guy1_up": 
					setAnim ("guy1_idle_up");
					break;
				case "guy1_down": 
					setAnim ("guy1_idle_down");
					break;
				case "guy1_left": 
					setAnim ("guy1_idle_left");
					break;
				case "guy1_right": 
					setAnim ("guy1_idle_right");
					break;
			}
		}
	}

	void handleKeyControl() {
		if (Input.GetKey (KeyCode.UpArrow)) {
			walk ("up");
		} else if (Input.GetKey (KeyCode.DownArrow)) {
			walk ("down");
		} else if (Input.GetKey (KeyCode.LeftArrow)) {
			walk ("left");
		} else if (Input.GetKey (KeyCode.RightArrow)) {
			walk ("right");
		} else {
			Debug.Log(currentState);
			switch (currentState) {
			case "guy1_up": 
				setAnim ("guy1_idle_up");
				break;
			case "guy1_down": 
				setAnim ("guy1_idle_down");
				break;
			case "guy1_left": 
				setAnim ("guy1_idle_left");
				break;
			case "guy1_right": 
				setAnim ("guy1_idle_right");
				break;
			}
		}
	}

	IEnumerator Wait(){
		yield return new WaitForSeconds (1.0f);
	}
	
}
