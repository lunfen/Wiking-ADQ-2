using UnityEngine;
using System.Collections;

public class groundHit : MonoBehaviour
{
	public GameObject effect;
	GameObject player;

	void Awake ()
	{
		player = GameObject.Find("character");
	}
	
	void OnCollisionEnter2D (Collision2D coll)
	{
		if (coll.gameObject.tag == "hardsurface" || coll.gameObject.tag == "wall")
		{
			GameObject clone = Instantiate(effect, new Vector3(coll.contacts[0].point.x, coll.contacts[0].point.y, 0) , Quaternion.identity) as GameObject;
			Destroy(clone, 1.1f);

			player.GetComponent<Rigidbody2D>().gravityScale = 2;
		}
	}

	void OnTriggerEnter2D (Collider2D coll)
	{
		if (coll.gameObject.tag == "green")
		{
			coll.gameObject.GetComponent<Animator>().SetTrigger("enter");
		}
	}
}
