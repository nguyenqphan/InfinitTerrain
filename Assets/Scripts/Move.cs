using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {

	public float Speed = 1f;
	public float FastSpeed = 3f;
	public KeyCode EnableFastSpeedWithKey = KeyCode.LeftShift;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		var currentSpeed = Speed;
		if(Input.GetKey(EnableFastSpeedWithKey)){
			currentSpeed = FastSpeed;
		}

		var movement = new Vector3 (Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
		transform.Translate (movement * currentSpeed * Time.deltaTime);
	}
}
