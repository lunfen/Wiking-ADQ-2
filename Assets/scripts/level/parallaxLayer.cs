using UnityEngine;
using System.Collections;

public class parallaxLayer : MonoBehaviour 
{
	public float dampTime = 0.15f;
	private Vector3 velocity = Vector3.zero;
//	public Transform target;
	public float levelShift;
	public float edgeShift;
	float shift;
	float startX;
	Camera mainCamera;

	void Awake ()
	{
		mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
		startX = mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0)).x;
	}

	void Update () 
	{
			shift = mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0)).x - startX - edgeShift;
			Vector3 _shift = new Vector3( shift*levelShift, 0, 0);

			Vector3 delta = mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 10)) - transform.position + _shift;
			Vector3 destination = transform.position + delta;
			transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime*Time.deltaTime);
	}
}
