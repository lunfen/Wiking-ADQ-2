using UnityEngine;
using System.Collections;

public class physicsJumper : MonoBehaviour
{

	void OnTriggerEnter2D (Collider2D coll)
	{
		if (coll.gameObject.tag == "Player")
		{
			coll.GetComponent<Rigidbody2D>().isKinematic = true;
			coll.GetComponent<Rigidbody2D>().isKinematic = false;
			coll.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 1600));
			coll.GetComponent<charController>().onHardSurface = false;
			coll.GetComponent<Animator>().SetBool("jump", true);
		}
	}
}
