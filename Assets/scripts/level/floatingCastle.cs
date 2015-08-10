using UnityEngine;
using System.Collections;

public class floatingCastle : MonoBehaviour 
{
	public float speed = 1;
	public float torque;
	Vector2 pos;
	public bool horizontal;


	void Awake ()
	{
		pos = transform.position;
	}

	void Update ()
	{
		pos = new Vector2(pos.x+ speed/20, pos.y);
//		transform.Translate(new Vector2(speed*Time.deltaTime, 0));
		transform.position = pos;
		if (!horizontal)
			transform.Rotate (new Vector3 (0, 0, torque));
		else
			transform.Rotate (new Vector3 (0, torque, 0));
	}
}
