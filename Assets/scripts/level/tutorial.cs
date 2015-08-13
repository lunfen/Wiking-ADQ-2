using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class tutorial : MonoBehaviour 
{
	charController player;
	physicsLog logcontroller;
	int stage;
	public GameObject[] button;
	public GameObject[] info;

	void Start () 
	{
		player = GameObject.Find("character").GetComponent<charController>();
		logcontroller = GameObject.Find("loghorController").GetComponent<physicsLog>();

		player.curJerks = 2;
		player.showJerk();
	}
	
	void Update () 
	{
		// show stage
		switch (stage)
		{
		// moving left or right
		case 0:
			logcontroller.button[2].GetComponent<Image>().color = new Color(0.3f,0.3f,0.3f,0.5f);
			logcontroller.button[3].GetComponent<Image>().color = new Color(0.3f,0.3f,0.3f,0.5f);
			logcontroller.button[4].GetComponent<Image>().color = new Color(0.3f,0.3f,0.3f,0.5f);
			logcontroller.button[5].GetComponent<Image>().color = new Color(0.3f,0.3f,0.3f,0.5f);
			button[2].SetActive(false);
			button[3].SetActive(false);
			button[4].SetActive(false);
			button[5].SetActive(false);
			info[0].SetActive(true);

			if (player.movingRight || player.movingLeft)
			{
				stage = 1;
				logcontroller.button[2].GetComponent<Image>().color = new Color(1,1,1,1);
				button[2].SetActive(true);
				info[0].GetComponent<Animator>().SetTrigger("out");
				info[1].SetActive(true);
			}
			break;
		//Jump
		case 1:
			if (player.pressedJump)
			{
				stage = 2;
				info[1].GetComponent<Animator>().SetTrigger("out");
			}
			break;
		//Jump off
		case 2:
			if (player.transform.position.x > -10)
			{
				stage = 3;
				info[2].SetActive(true);
			}
			break;
		// Double jump
		case 3:
			if (player.transform.position.x > -4)
			{
				stage = 4;
				info[2].GetComponent<Animator>().SetTrigger("out");
			}
			break;
		// attack
		case 4:
			if (player.transform.position.x > 18)
			{
				stage = 5;
				logcontroller.button[3].GetComponent<Image>().color = new Color(1,1,1,1);
				button[3].SetActive(true);
				info[3].SetActive(true);
			}
			break;
		// kill enemy
		case 5:
			if (!GameObject.FindGameObjectWithTag("death25"))
			{
				stage = 6;
				info[3].GetComponent<Animator>().SetTrigger("out");
			}
			break;
			// Jerk
		case 6:
			if (player.transform.position.x > 35)
			{
				stage = 7;
				logcontroller.button[5].GetComponent<Image>().color = new Color(1,1,1,1);
				button[5].SetActive(true);
				info[4].SetActive(true);
			}
			break;
			// Jerk off
		case 7:
			if (player.transform.position.x > 43)
			{
				stage = 8;
				info[4].GetComponent<Animator>().SetTrigger("out");
			}
			break;
			// Throw axe
		case 8:
			if (player.transform.position.x > 54)
			{
				stage = 9;
				logcontroller.button[4].GetComponent<Image>().color = new Color(1,1,1,1);
				button[4].SetActive(true);
				logcontroller.isTutorial = false;
				info[5].SetActive(true);
			}
			break;
		case 9:
			if (player._hanging)
			{
				stage = 10;
				info[6].SetActive(true);
				info[5].GetComponent<Animator>().SetTrigger("out");
			}
			break;
		case 10:
			if (!player._hanging)
			{
				stage = 11;
				info[6].GetComponent<Animator>().SetTrigger("out");
			}
			break;
		case 11:
			if (player.transform.position.x > 68)
			{
				stage = 12;
				info[7].SetActive(true);
			}
			break;
		case 12:
			if (player.transform.position.x > 72)
			{
				stage = 13;
				info[7].GetComponent<Animator>().SetTrigger("out");
			}
			break;
		}

	}
}
