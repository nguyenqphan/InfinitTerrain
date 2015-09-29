using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {
	public float Speed = 1;
	public float FastSpeed = 3;
	public KeyCode EnableFastSpeedWithKey = KeyCode.LeftShift;
	public Transform TurnWithMovement;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		var currentSpeed = Speed;
		if (Input.GetKey (EnableFastSpeedWithKey)) {
			currentSpeed = FastSpeed;
		}
		var movement = new Vector3 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"), 0);
		transform.Translate (movement * currentSpeed * Time.deltaTime);

		//turn the player to the direction that it is heading.
		if (movement.x > 0.1f || movement.x < -0.1f || movement.y > 0.1f || movement.y < -0.1f) {
			TurnWithMovement.rotation = Quaternion.LookRotation (Vector3.back, movement.normalized);
		}
	}
}
