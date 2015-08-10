using UnityEngine;
using System.Collections;

public class groundController : MonoBehaviour
{

	GameObject player;

	public GameObject dropEffect;
	
	void Awake ()
	{
		player = GameObject.Find("character");
	}
	
	void OnTriggerEnter2D (Collider2D coll)
	{
		if (coll.gameObject.tag == "groundCheck")
		{
			player.GetComponent<charController>().onHardSurface = true;
			player.GetComponent<Animator>().SetBool("jump",false);
			player.GetComponent<charController>().doubleJump = false;

			//GameObject clone = Instantiate(dropEffect, new Vector3(coll.contacts[0].point.x, coll.contacts[0].point.y, 0) , Quaternion.identity) as GameObject;
			GameObject clone = Instantiate(dropEffect, coll.transform.position , Quaternion.identity) as GameObject;
			Destroy(clone, 1.1f);
		}
		
		if (player.GetComponent<charController>().movingLeft || player.GetComponent<charController>().movingRight)
			player.GetComponent<Animator>().SetBool("walk",true);
		
	}
	
	void OnTriggerStay2D (Collider2D coll)
	{
		if (coll.gameObject.tag == "groundCheck")
		{
			player.GetComponent<charController>().onHardSurface = true;
			player.GetComponent<Animator>().SetBool("jump",false);
		}
		else
		{
		}
	}

	void OnTriggerExit2D (Collider2D coll)
	{
		if (coll.gameObject.tag == "groundCheck")
		{
			player.GetComponent<charController>().onHardSurface = false;
			player.GetComponent<Animator>().SetBool("walk",false);
			player.GetComponent<Animator>().SetBool("jump",true);

		}
	}

}
