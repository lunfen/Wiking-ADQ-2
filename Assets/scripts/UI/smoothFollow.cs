using UnityEngine;
using System.Collections;
	
public class smoothFollow : MonoBehaviour
{
		
	public float dampTime = 0.15f;
	private Vector3 velocity = Vector3.zero;
	Transform target;
	public Transform[] points;
	int index;
	float bestDistance;
	Transform player;

	void Awake ()
	{
		player = GameObject.Find("character").transform;
	}
	
	void Update () 
	{
		// start
		bestDistance = Vector2.Distance(player.position, points[0].position);
		index = 0;

		// select closest target
		for (int i = 1; i < points.Length; i++)
		{
			float distance = Vector2.Distance(player.position, points[i].position);
			if (distance < bestDistance)
				{
				bestDistance = distance;
				index = i;
				}
		}

		target = points[index];

		if (target.GetComponent<CircleCollider2D>().radius < bestDistance)
			target = player.transform;

		// smooth follow apply
		if (target)
		{
			Vector3 point = GetComponent<Camera>().WorldToViewportPoint(target.position);
			Vector3 delta = target.position - GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
			Vector3 destination = transform.position + delta;
			transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
		}

		// check dead
		if (player.GetComponent<charController>().deadTimer > 0.4f)
			Application.LoadLevel(Application.loadedLevel);
		else if (player.GetComponent<charController>().deadTimer > 0)
			player.GetComponent<charController>().deadTimer += Time.deltaTime;

	}
}
