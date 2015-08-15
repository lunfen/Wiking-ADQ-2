using UnityEngine;
using System.Collections;

public class pickItem : MonoBehaviour 
{

	public bool jerk;
	public bool hp;

	void OnTriggerEnter2D (Collider2D coll)
	{
		if (coll.gameObject.tag == "Player")
		{
			Destroy(gameObject);

			if (jerk)
				coll.GetComponent<charController>().AddJerks (1);

			if (hp)
				coll.GetComponent<charController>().AddHealth (25);
		}
	}
}
