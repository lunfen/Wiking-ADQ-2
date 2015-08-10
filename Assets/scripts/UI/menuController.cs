using UnityEngine;
using System.Collections;

public class menuController : MonoBehaviour
{


	public void panelHide (GameObject panel)
	{
		panel.GetComponent<Animator>().SetBool("hide", true);
	}
}
