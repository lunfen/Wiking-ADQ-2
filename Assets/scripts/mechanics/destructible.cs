using UnityEngine;
using System.Collections;

public class destructible : MonoBehaviour 
{
	SpriteRenderer graphic;
	ParticleSystem effect;
	Rigidbody2D rbody;
	float alpha;

	void Start () 
	{
		graphic = GetComponent<SpriteRenderer>();
		rbody = GetComponent<Rigidbody2D>();
		effect = transform.GetChild(0).GetComponent<ParticleSystem>();

		effect.enableEmission = false;
		alpha = 1;
	}

	void Update () 
	{
		if (!rbody.isKinematic)
		{
			effect.enableEmission = true;
			effect.emissionRate -= 3;
			alpha -= 0.01f;
			graphic.color = new Color(1,1,1,alpha); 
			if (alpha <= 0)
				Destroy(gameObject, 7);
			else if (alpha < 0.3f)
				GetComponent<PolygonCollider2D>().enabled = false;
		}
	}
}
