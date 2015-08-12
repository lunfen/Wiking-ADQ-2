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
	RectTransform levelMask;
	bool switchDampTime;

	void Awake ()
	{
		player = GameObject.Find("character").transform;
		levelMask = GameObject.Find("LevelMask").GetComponent<RectTransform>();

		//fix respawn bug
		player.gameObject.GetComponent<charController>().moveRight();
		player.gameObject.GetComponent<charController>().moveNone();
	}
	
	void Update () 
	{
		// level mask anim
		if (player.gameObject.GetComponent<charController>().deadTimer == 0)
		{
			if (levelMask.sizeDelta.x > 0)
				levelMask.sizeDelta = new Vector2(levelMask.sizeDelta.x-50, levelMask.sizeDelta.x-50);
		}
		else
			levelMask.sizeDelta = new Vector2(levelMask.sizeDelta.x+50, levelMask.sizeDelta.x+50);

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
		{
			target = player.transform;
			if (dampTime > 2f)
				switchDampTime = true;
		}
		else if (dampTime < 2)
			dampTime += 0.05f;

		if (switchDampTime)
			if (dampTime > 0.2f)
				dampTime -= 0.05f;
			else
				switchDampTime = false;

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

	void OnGUI()
	{
		GUI.Label(new Rect(150,20,100,100), "damp time " + dampTime);
	}
}
