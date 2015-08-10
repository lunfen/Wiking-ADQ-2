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
	}
	
	void Update () 
	{
		// show stage
		switch (stage)
		{
		case 0:
			logcontroller.button[0].GetComponent<Image>().color = new Color(0.3f,0.3f,0.3f,0.5f);
			logcontroller.button[2].GetComponent<Image>().color = new Color(0.3f,0.3f,0.3f,0.5f);
			logcontroller.button[3].GetComponent<Image>().color = new Color(0.3f,0.3f,0.3f,0.5f);
			logcontroller.button[4].GetComponent<Image>().color = new Color(0.3f,0.3f,0.3f,0.5f);
			logcontroller.button[5].GetComponent<Image>().color = new Color(0.3f,0.3f,0.3f,0.5f);
			button[0].SetActive(false);
			button[2].SetActive(false);
			button[3].SetActive(false);
			button[4].SetActive(false);
			button[5].SetActive(false);
			info[0].SetActive(true);

			if (player.movingRight)
			{
				stage = 1;
				logcontroller.button[0].GetComponent<Image>().color = new Color(1,1,1,1);
				button[0].SetActive(true);
				info[0].SetActive(false);
				info[1].SetActive(true);
			}
			break;
		case 1:
			if (player.movingLeft)
			{
				stage = 2;
				logcontroller.button[2].GetComponent<Image>().color = new Color(1,1,1,1);
				button[2].SetActive(true);
				info[1].SetActive(false);
				info[2].SetActive(true);
			}
			break;
		case 2:
			if (player.pressedJump)
			{
				stage = 3;
				logcontroller.button[3].GetComponent<Image>().color = new Color(1,1,1,1);
				button[3].SetActive(true);
				info[2].SetActive(false);
				info[3].SetActive(true);
			}
			break;
		case 3:
			if (player.transform.position.x > -2.5)
			{
				stage = 4;
				info[3].SetActive(false);
				info[4].SetActive(true);
			}
			break;
		case 4:
			if (!GameObject.FindGameObjectWithTag("death25"))
			{
				stage = 5;
				info[4].SetActive(false);
				info[7].SetActive(true);
				info[8].SetActive(true);
				logcontroller.button[5].GetComponent<Image>().color = new Color(1,1,1,1);
				button[5].SetActive(true);
			}
			break;
		case 5:
			if (player.pressedJerk)
			{
				stage = 6;
				info[7].SetActive(false);
				info[8].SetActive(false);
				info[5].SetActive(true);
				logcontroller.isTutorial = false;
			}
			break;
		case 6:
			if (player._hanging)
			{
				stage = 7;
				info[5].SetActive(false);
				info[6].SetActive(true);
			}
			break;
		case 7:
			if (player.transform.position.x > 55)
			{
				stage = 8;
				info[7].SetActive(false);
				info[8].SetActive(false);
			}
			break;
		}

	}
}
