using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class physicsLog : MonoBehaviour
{
	public GameObject player;
	public GameObject buttonAxe;
	public GameObject[] logHors;
	public GameObject[] button;
	int closest;
	float shortestDistance;
	public bool isTutorial;

	void Update ()
	{
		// check logs in scene
		closest = 0;
		shortestDistance = Vector2.Distance(logHors[0].transform.position, player.transform.position);

		for (int i = 1; i < logHors.Length; i++)
		{
			float distance = Vector2.Distance(logHors[i].transform.position, player.transform.position);
			if (distance < shortestDistance)
			{
				shortestDistance = distance;
				closest = i;
			}
		}

		// activate/deactivate button
		if (shortestDistance < 6)
		{
			player.GetComponent<charController>().transformThrowAxe = logHors[closest].transform;
			buttonAxe.SetActive(true);
		}
		else
		{
			player.GetComponent<charController>().transformThrowAxe = null;
			buttonAxe.SetActive(false);
		}

		// change color of pressed buttons
		if (!isTutorial)
		{
		if (player.GetComponent<charController>().movingLeft)
			button[0].GetComponent<Image>().color = new Color(0.8f,1,1,1);
		else
			button[0].GetComponent<Image>().color = new Color(1,1,1,1);
		
		if (player.GetComponent<charController>().movingRight)
			button[1].GetComponent<Image>().color = new Color(0.8f,1,1,1);
		else
			button[1].GetComponent<Image>().color = new Color(1,1,1,1);
		
		if (player.GetComponent<charController>().pressedJump)
			button[2].GetComponent<Image>().color = new Color(0.5f,1,0.5f,1);
		else
			button[2].GetComponent<Image>().color = new Color(1,1,1,1);
		
		if (player.GetComponent<charController>().pressedAttack)
			button[3].GetComponent<Image>().color = new Color(0,1,1,1);
		else
			button[3].GetComponent<Image>().color = new Color(1,1,1,1);
		
		if (player.GetComponent<charController>().pressedThrow)
			button[4].GetComponent<Image>().color = new Color(1,0,1,1);
		else if (shortestDistance < 6)
			button[4].GetComponent<Image>().color = new Color(1,1,1,1);
		else
			button[4].GetComponent<Image>().color = new Color(0.4f,0.4f,0.4f,1);

		if (player.GetComponent<charController>().pressedJerk)
			button[5].GetComponent<Image>().color = new Color(1,1,0,1);
		else if (player.GetComponent<charController>().jerkPower >= 1)
			button[5].GetComponent<Image>().color = new Color(1,1,1,1);
		else
			button[5].GetComponent<Image>().color = new Color(0.4f,0.4f,0.4f,1);
		}
		else
		{

		}
	}
}
