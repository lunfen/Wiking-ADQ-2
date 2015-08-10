using UnityEngine;
using System.Collections;

public class axeAttack : MonoBehaviour
{
	GameObject player;

	void Awake()
	{
		player = GameObject.Find("character");
	}

	void Update ()
	{
		if (player.GetComponent<charController>().deadTimer > 0)
		{
			gameObject.transform.SetParent(null);
			transform.Translate(new Vector2(0, Time.deltaTime* -5));
			transform.Rotate(new Vector3(0, 0, Time.deltaTime* -50));
		}
	}

	void OnTriggerEnter2D (Collider2D coll)
	{
		if (coll.gameObject.tag == "Enemy")
		{
			Destroy(coll.gameObject,5);
			if (coll.GetComponent<ParticleSystem>())
			{
			coll.GetComponent<ParticleSystem>().enableEmission = false;

			coll.transform.GetChild(0).GetComponent<PolygonCollider2D>().isTrigger = false;
			coll.transform.GetChild(0).GetComponent<Rigidbody2D>().isKinematic = false;
			coll.transform.GetChild(0).GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 300));

			coll.transform.GetChild(0).GetComponent<Animator>().enabled = false;
			coll.transform.GetChild(0).gameObject.tag = "Untagged";

			coll.transform.GetChild(0).GetComponent<enemyController>().enabled = false;
			}
		}
	}
}
