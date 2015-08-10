using UnityEngine;
using System.Collections;

public class physicsChainElement : MonoBehaviour
{
	Transform chain_root;
	
	void Start ()
	{
		chain_root = GameObject.Find("chain_root").transform;
	}

	void Update ()
	{
		float distance = Vector2.Distance(transform.position, chain_root.position);
		float length = transform.parent.GetComponent<physicsChain>().distance;

		if (distance > length)
			GetComponent<SpriteRenderer>().enabled = false;
		else
			GetComponent<SpriteRenderer>().enabled = true;
	}

}
