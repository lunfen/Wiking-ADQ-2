using UnityEngine;
using System.Collections;

public class enemyController : MonoBehaviour
{
	GameObject root;
	bool movingRight;
	float locker;
	public bool isStatic;

	void Start ()
	{
		root = transform.parent.gameObject;
	}
	
	void Update ()
	{
		// moving mechanics
		if (!isStatic)
		if (movingRight)
			{
			root.transform.Translate(new Vector2(0.02f, 0));
			root.transform.localScale = new Vector2(-1,1);
			}
		else
			{
			root.transform.Translate(new Vector2(-0.02f, 0));
			root.transform.localScale = new Vector2(1,1);
			}

		if (locker > 0)
			locker -= Time.deltaTime;
	}

	void OnTriggerEnter2D (Collider2D coll)
	{
		if (coll.gameObject.tag == "platformEnd")
		{
			movingRight = !movingRight;
			locker = 0.5f;
		}
	}
}
