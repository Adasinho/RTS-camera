using UnityEngine;
using System.Collections;

public class RTSCamera : MonoBehaviour {

	public Transform gameObj;

	bool moveForward = false;
	bool moveBackward = false;
	bool moveRight = false;
	bool moveLeft = false;

	bool rotationLeft = false;
	bool rotationRight = false;

	public float speedCamera;
	public float speedScroll;
	public float speedRotate;

	public float cameraDistance = 12.5f;
	public float cameraForwardDistance = 10f;

	//internal check
	float curDistance;
	float curForwardDistance;

	RaycastHit hit;
	RaycastHit hitForward;

	// Update is called once per frame
	void Update () {
		RTSCam();
	}

	void RTSCam()
	{
		#region Move
		if(Input.GetKeyDown(KeyCode.W)) moveForward = true;
		if(Input.GetKeyUp(KeyCode.W)) moveForward = false;

		if(Input.GetKeyDown(KeyCode.S)) moveBackward = true;
		if(Input.GetKeyUp(KeyCode.S)) moveBackward = false;

		if(Input.GetKeyDown(KeyCode.A)) moveLeft = true;
		if(Input.GetKeyUp(KeyCode.A)) moveLeft = false;

		if(Input.GetKeyDown(KeyCode.D)) moveRight = true;
		if(Input.GetKeyUp(KeyCode.D)) moveRight = false;
		#endregion

		#region Rotation
		if(Input.GetKeyDown(KeyCode.E)) rotationLeft = true;
		if(Input.GetKeyUp(KeyCode.E)) rotationLeft = false;

		if(Input.GetKeyDown(KeyCode.Q)) rotationRight = true;
		if(Input.GetKeyUp(KeyCode.Q)) rotationRight = false;
		#endregion


		if(Physics.Raycast(gameObj.transform.position, -gameObj.transform.up, out hit, 100))
		{
			curDistance = Vector3.Distance(gameObj.transform.position, hit.point);
			Debug.DrawLine(gameObj.transform.position, hit.point);
		}


		if(Physics.Raycast(transform.position, transform.forward, out hitForward, 50))
		{
			curForwardDistance = Vector3.Distance(gameObj.transform.position, hitForward.point);
			Debug.DrawLine(gameObj.transform.position, hitForward.point, Color.green);
		}

		MoveCamera();
	}

	void MoveCamera()
	{
		if(moveForward) gameObj.Translate(Vector3.forward * speedCamera * Time.deltaTime,Space.Self);
		if(moveBackward) gameObj.Translate(Vector3.back * speedCamera * Time.deltaTime,Space.Self);
		if(moveLeft) gameObj.Translate(Vector3.left * speedCamera * Time.deltaTime,Space.Self);
		if(moveRight) gameObj.Translate(Vector3.right * speedCamera * Time.deltaTime,Space.Self);

		if(curDistance < cameraDistance)
		{
			float difference = cameraDistance - curDistance;

			gameObj.transform.position = Vector3.Lerp (gameObj.transform.position, gameObj.transform.position + new Vector3(0f,difference,0f),Time.deltaTime);
		}

		if(curForwardDistance < cameraForwardDistance)
		{
			float differenceForward = cameraForwardDistance - curForwardDistance;

			gameObj.transform.position = Vector3.Lerp(gameObj.transform.position, transform.position + (transform.forward * -differenceForward), Time.deltaTime);		
			Debug.DrawLine(gameObj.transform.position, transform.position + (transform.forward * -differenceForward), Color.yellow);
		}


		if(((gameObj.transform.position.y > 3) || (Input.GetAxis("Mouse ScrollWheel") < 0)) && ((gameObj.transform.position.y < 19) || (Input.GetAxis("Mouse ScrollWheel") > 0))) gameObj.transform.Translate(transform.forward * Input.GetAxis("Mouse ScrollWheel") * speedScroll * Time.deltaTime,Space.World);

		//if(rotationLeft) gameObj.Rotate(0f,-speedRotate * Time.deltaTime,0f,Space.World);
		//if(rotationRight) gameObj.Rotate(0f,speedRotate * Time.deltaTime,0f,Space.World);
		if(rotationLeft) gameObj.RotateAround(hitForward.point, Vector3.up, Time.deltaTime * speedRotate);
		if(rotationRight) gameObj.RotateAround(hitForward.point, -Vector3.up, Time.deltaTime * speedRotate);
	}
}