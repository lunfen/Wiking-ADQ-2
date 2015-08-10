using UnityEngine;
using System.Collections;

public class destructiblewall : MonoBehaviour
{
	public GameObject effect;

	void OnCollisionEnter2D (Collision2D coll)
	{
		if (coll.gameObject.tag == "Player")
			if (coll.gameObject.GetComponent<charController>().jerkTimer > 0)
			{
				GameObject clone = Instantiate(effect, coll.transform.position, Quaternion.identity) as GameObject;
				Destroy(clone, 1);

				GetComponent<BoxCollider2D>().enabled = false;

				PolygonCollider2D[] colliders = GetComponentsInChildren<PolygonCollider2D>();
				Rigidbody2D[] rigidbodies = GetComponentsInChildren<Rigidbody2D>();
			
				foreach (PolygonCollider2D collider in colliders)
					collider.isTrigger = false;
				
				foreach (Rigidbody2D rigidbody in rigidbodies)
				{
					rigidbody.isKinematic = false;

					float force = transform.position.x - coll.transform.position.x;
					rigidbody.AddForceAtPosition( new Vector2(force * 250, 40), transform.position );
				}
		}
	}
}
