using UnityEngine;
using System.Collections;

public class keyboard : MonoBehaviour 
{

	void Update () 
	{
		// moving Left
		if (Input.GetKeyDown(KeyCode.A))
			GetComponent<charController>().moveLeft ();

		if (Input.GetKeyUp(KeyCode.A))
			GetComponent<charController>().moveNone ();
		
		// moving Right
		if (Input.GetKeyDown(KeyCode.D))
			GetComponent<charController>().moveRight ();
		
		if (Input.GetKeyUp(KeyCode.D))
			GetComponent<charController>().moveNone ();
		
		// jumping
		if (Input.GetKeyDown(KeyCode.Space))
			GetComponent<charController>().Jump ();
		
		if (Input.GetKeyUp(KeyCode.Space))
			GetComponent<charController>().JumpReleased ();
		
		// attack
		if (Input.GetKeyDown(KeyCode.M))
			GetComponent<charController>().Attack ();
		
		if (Input.GetKeyUp(KeyCode.M))
			GetComponent<charController>().AttackReleased ();
		
		// throw
		if (Input.GetKeyDown(KeyCode.K))
			GetComponent<charController>().useAxe ();
		
		if (Input.GetKeyUp(KeyCode.K))
			GetComponent<charController>().useAxeReleased ();
		
		// jerk
		if (Input.GetKeyDown(KeyCode.L))
			GetComponent<charController>().useJerk ();
		
		if (Input.GetKeyUp(KeyCode.L))
			GetComponent<charController>().useJerkReleased ();
	}
}
