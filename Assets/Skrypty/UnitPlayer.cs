using UnityEngine;
using System.Collections;

public class UnitPlayer : Unit {

	//float cameraRotX = 0f;

	public float cameraPitchMax = 45f;

	// Use this for initialization
	public override void Start () {
		base.Start();
	}
	
	// Update is called once per frame
	public override void Update () {

		#region rotation

		transform.Rotate(0f, Input.GetAxis("Mouse X") * turnSpeed * Time.deltaTime, 0f);

		#endregion

		#region movement

		move = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

		move.Normalize();

		move = transform.TransformDirection (move);

		if(Input.GetKeyDown(KeyCode.Space) && (control.isGrounded))
		{
			jump = true;
		}

		#endregion

		base.Update();
	}
}
