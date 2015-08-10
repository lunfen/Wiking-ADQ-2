using UnityEngine;
using System.Collections;

public class physicsAxe : MonoBehaviour
{
	void OnTriggerEnter2D (Collider2D coll)
	{
		if (coll.gameObject.tag == "log")
			{
			GetComponent<Rigidbody2D>().isKinematic = true;
			GetComponent<SpringJoint2D>().enabled = true;
			GetComponent<SpringJoint2D>().connectedBody = GameObject.Find("character").GetComponent<Rigidbody2D>();
			coll.transform.parent.GetComponent<physicsLog>().player.GetComponent<charController>().transformThrowAxe = null;
			coll.transform.parent.GetComponent<physicsLog>().player.GetComponent<Animator>().SetBool("hanging",true);
			coll.transform.parent.GetComponent<physicsLog>().player.GetComponent<charController>()._hanging = true;
			coll.transform.parent.GetComponent<physicsLog>().player.GetComponent<Rigidbody2D>().gravityScale = 2;
			coll.transform.parent.GetComponent<physicsLog>().player.GetComponent<Rigidbody2D>().drag = 0;
			GetComponent<SpringJoint2D>().distance = Vector2.Distance(transform.position, coll.transform.parent.GetComponent<physicsLog>().player.transform.position);
			}
	}

	void OnTriggerStay2D (Collider2D coll)
	{
		if (coll.gameObject.tag == "log")
			if (!GetComponent<SpringJoint2D>().enabled)
			Destroy(gameObject);
	}

	void Update ()
	{
		if (GetComponent<Rigidbody2D>().isKinematic)
		{
			if (GetComponent<SpringJoint2D>().distance > 3.7f)
				GetComponent<SpringJoint2D>().distance -= 0.1f;
			else if (GetComponent<SpringJoint2D>().distance < 3.5f)
				GetComponent<SpringJoint2D>().distance += 0.1f;
		}
	}
}
