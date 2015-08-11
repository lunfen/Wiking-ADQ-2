using UnityEngine;
using System.Collections;

public class physicsChain : MonoBehaviour
{
	GameObject player;
	public float distance;

	void Start ()
	{
		player = GameObject.Find("bone_10");
	}

	void Update ()
	{
		distance = Vector2.Distance(transform.position, player.transform.position);

		float dx = player.transform.position.x - transform.position.x;
		float dy = player.transform.position.y - transform.position.y;
		float rotAngle = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg -90;

		transform.rotation = Quaternion.Euler( new Vector3(0,0,rotAngle) );
	}

}
