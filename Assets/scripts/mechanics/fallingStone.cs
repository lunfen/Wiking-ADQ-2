using UnityEngine;
using System.Collections;

public class fallingStone : MonoBehaviour
{
	float _delay;

	void Update ()
	{
		if (_delay > 0)
			_delay -= Time.deltaTime;

		if (_delay < 0)
			GetComponent<Rigidbody2D>().isKinematic = false;
	}
	
	void OnCollisionEnter2D (Collision2D coll)
	{
		if (coll.gameObject.tag == "Player")
			if (coll.transform.position.y > transform.position.y)
				_delay = 0.35f;
	}
}
