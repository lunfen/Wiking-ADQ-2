using UnityEngine;
using System.Collections;

public class physicsJumper : MonoBehaviour
{
	public int force = 1000;

	void OnTriggerEnter2D (Collider2D coll)
	{
		if (coll.gameObject.tag == "Player")
		{
			coll.GetComponent<Rigidbody2D>().isKinematic = true;
			coll.GetComponent<Rigidbody2D>().isKinematic = false;
			coll.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, force));
			coll.GetComponent<charController>().onHardSurface = false;
			coll.GetComponent<Animator>().SetBool("jump", true);
		}
	}
}
