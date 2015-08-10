using UnityEngine;
using System.Collections;

public class physicsPlatform : MonoBehaviour
{
	GameObject player;

	void Awake ()
	{
		player = GameObject.Find("groundCheck");
	}

	void Update ()
	{
		if (player.transform.position.y > transform.position.y)
			GetComponent<PolygonCollider2D>().enabled = true;
		else
			GetComponent<PolygonCollider2D>().enabled = false;
	}

	/*
	void OnTriggerEnter2D (Collider2D coll)
	{
		if (coll.gameObject.tag == "groundCheck")
			{
			//coll.transform.parent.transform.Translate(new Vector2(0.2f, 0));
			GetComponent<PolygonCollider2D>().isTrigger = false;
			}
	}
	
	void OnTriggerExit2D (Collider2D coll)
	{
		if (coll.gameObject.tag == "groundCheck")
			GetComponent<PolygonCollider2D>().isTrigger = true;
	}
	*/
}
