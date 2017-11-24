using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

	CharacterController cc;

	public float MoveSpeed;
	public float RotationSpeed;
	public float jumpSpeed;

	public bool jump = false;

	public Vector3 move = Vector3.zero;
	public Vector3 gravity = Vector3.zero;

	// Use this for initialization
	void Start () {
		cc = GetComponent<CharacterController>();

		if(!cc)
		{
			Debug.LogError("Movement.Start() " + name + "has no CharacterController!");
			enabled = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 forward = Input.GetAxis ("Vertical") * transform.TransformDirection (Vector3.forward) * MoveSpeed;
		transform.Rotate(new Vector3(0,Input.GetAxis("Horizontal") * RotationSpeed * Time.deltaTime,0));

		cc.Move (forward * Time.deltaTime);
		cc.SimpleMove (Physics.gravity);

		if((Input.GetKey(KeyCode.Space)))
	 	{
			jump = true;
		}

		if(!cc.isGrounded)
		{
			gravity += Physics.gravity * Time.deltaTime;
		} else {
			gravity = Vector3.zero;

			if(jump)
			{
				gravity.y = jumpSpeed;
				jump = false;
			}
		}
	}
}
