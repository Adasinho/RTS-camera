using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

	public GameObject swiatlo;
	Light swiatelo;
	public Camera [] camers;
	int index;
	int number_of_camers;

	void Awake()
	{
		number_of_camers = Camera.allCameras.Length;
	}

	// Use this for initialization
	void Start () {
		for(int k = 0; k < number_of_camers; k++)
			camers[k].enabled = false;
		camers[0].enabled = true;
		index = 0;
	}

	void Update () {
		if(Input.GetKeyDown(KeyCode.C)) SelectCamera();
		if(Input.GetKeyDown(KeyCode.N)) LightOffOn();
	}

	public void LightOffOn()
	{
		swiatelo = swiatlo.GetComponent<Light>();
		if(swiatelo.enabled) swiatelo.enabled = false;
			else swiatelo.enabled = true;
	}

	public void SelectCamera()
	{
		Debug.Log(index);
		camers[index].enabled = false;
		if(index == number_of_camers-1) index = 0;
			else index++;
		camers[index].enabled = true;
	}
}
