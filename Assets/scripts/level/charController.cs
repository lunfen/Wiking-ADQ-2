using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class charController : MonoBehaviour
{
	int health = 100;
	float deadforce;
	public Transform hpLine;
	float hplineOffset;

	// Moving
	bool jumping;
	public bool doubleJump;
	public GameObject doubleJumpEffect;
	public bool movingLeft;
	public bool movingRight;
	public bool onHardSurface;
	GameObject floatingEffect;

	public bool pressedJump;
	public bool pressedJerk;
	public bool pressedAttack;
	public bool pressedThrow;

	// Jerk
	public float jerkPower = 1;
	public float jerkTimer;
	GameObject jerkButton;
	GameObject jerkEffect;
	public Texture[] jerkTexture;

	// Axe
	bool throwAxe;
	public GameObject prefabAxe;
	public bool _hanging;
	public Transform throwAxePoint;
//	public SpriteRenderer axeGraphic;
	public SkinnedMeshRenderer axeGraphic;
	public Transform transformThrowAxe;
	GameObject axeTrail;
	float attack;

	// specials
	public float deadTimer;
	float lockdamageTimer;

	void Awake ()
	{
		axeTrail = GameObject.Find("axeTrail");
		axeTrail.SetActive(false);

		jerkButton = GameObject.Find("Jerk");
		jerkEffect = GameObject.Find("jerkEffect");
		jerkEffect.SetActive(false);

		floatingEffect = GameObject.Find("fly_flash");
		floatingEffect.SetActive(false);

		Time.timeScale = 1;
	}

	void Update ()
	{
		// move & jump
		if (GetComponent<Rigidbody2D>().gravityScale > 0.1f && jerkTimer <= 0 && attack <= 0)
		{

			if (movingLeft || movingRight)
				if (attack <= 0)
					transform.Translate(new Vector3(0.08f, 0, 0));

			// moving bug fix
			if (transform.localScale.z == 0)
				transform.localRotation = Quaternion.Euler(0, 0, 0);
			else
				transform.localRotation = Quaternion.Euler(0, 180, 0);

			if (jumping)
			{
				onHardSurface = false;
			if (!_hanging)
				{
					GetComponent<Rigidbody2D>().isKinematic = true;
					GetComponent<Rigidbody2D>().isKinematic = false;
					GetComponent<Rigidbody2D>().AddForce(new Vector2(0,500));
				}
			
			if (GetComponent<Animator>().GetBool("jump"))
					GetComponent<Animator>().Play("jump 1",-1,0f);
			else
				GetComponent<Animator>().SetBool("jump",true);

			jumping = false;
			}
		}

		// Throw Axe
		if (throwAxe)
		{
			GameObject clone = Instantiate(prefabAxe, throwAxePoint.position, Quaternion.identity) as GameObject;
			Vector2 force = 50* (transformThrowAxe.position - GameObject.Find("bone_10").transform.position);

			axeGraphic.enabled = false;

			clone.GetComponent<Rigidbody2D>().AddForce(force);
			throwAxe = false;

			floatingEffect.SetActive(true);
		}

		if (_hanging)
			floatingEffect.SetActive(false);

		// Jerk
		if (jerkPower < 1)
			{
			jerkPower += 0.005f;
			jerkButton.GetComponent<Image>().color = new Color(1,1,1,jerkPower);
			}

		if (jerkTimer > 0)
			{
			jerkTimer -= Time.deltaTime;
			transform.Translate(new Vector2(transform.localScale.x, 0));
			GetComponent<Rigidbody2D>().gravityScale = 0;
			}
		else if (GetComponent<Rigidbody2D>().gravityScale == 0)
				{
				GetComponent<Rigidbody2D>().gravityScale = 2;
				GetComponent<Rigidbody2D>().isKinematic = true;
				GetComponent<Rigidbody2D>().isKinematic = false;

				if (transform.localScale.z == 0)
					GetComponent<Rigidbody2D>().AddForce(new Vector2(300, 0));
				else
					GetComponent<Rigidbody2D>().AddForce(new Vector2(-300, 0));

				// jerk off
				SkinnedMeshRenderer[] graphics = GetComponentsInChildren<SkinnedMeshRenderer>();
				foreach (SkinnedMeshRenderer graphic in graphics)
					graphic.enabled = true;

				jerkEffect.SetActive(false);
				}

		// Attack
		if (attack > 0)
		{
			transform.Translate(new Vector2(transform.localScale.x * 0.1f, 0));
			attack -= Time.deltaTime;
			axeTrail.SetActive(true);
			axeTrail.transform.parent.GetComponent<CircleCollider2D>().enabled = true;
		}
		else
			GetComponent<Animator>().SetBool("attack", false);

		if (attack < -0.5f)
		{
			axeTrail.transform.parent.GetComponent<CircleCollider2D>().enabled = false;
			axeTrail.SetActive(false);
		}
		else
			attack -= Time.deltaTime;

		if (transform.position.y < -50)
			Application.LoadLevel(Application.loadedLevel);

		// Check health
		if (health <= 0)
		{
			deadTimer = 0.01f;
			if (GameObject.Find("CanvasControls"))
				GameObject.Find("CanvasControls").SetActive(false);
			Time.timeScale = 0.1f;

			GetComponent<Rigidbody2D>().freezeRotation = false;
			GetComponent<PolygonCollider2D>().enabled = false;
			GetComponent<Animator>().SetTrigger("dead");
			
			enabled = false;
		}

		if (deadforce != 0)
		{
			GetComponent<Rigidbody2D>().isKinematic = true;
			GetComponent<Rigidbody2D>().isKinematic = false;
			GetComponent<Rigidbody2D>().AddForce( new Vector2(deadforce*700, 200) );
			deadforce = 0;
		}

		// Additional
		if (hpLine.transform.localPosition.x - 277 > hplineOffset)
			hpLine.Translate(new Vector3(-1,0,0));

		if (lockdamageTimer > 0)
			lockdamageTimer -= Time.deltaTime;
	}
	
	public void moveLeft ()
	{
		if (onHardSurface)
			GetComponent<Animator>().SetBool("walk",true);
		transform.localScale = new Vector3(0.25f,0.25f,-1);
		movingLeft = true;
		movingRight = false;
	}
	
	public void moveRight ()
	{
		if (onHardSurface)
			GetComponent<Animator>().SetBool("walk",true);
		transform.localScale = new Vector3(0.25f,0.25f,0);
		movingRight = true;
		movingLeft = false;
	}

	public void moveNone()
	{
		if (GetComponent<Animator>())
			GetComponent<Animator>().SetBool("walk",false);
		movingLeft = false;
		movingRight = false;
	}

	public void Jump ()
	{
		pressedJump = true;

		if (onHardSurface)
			jumping = true;
		else if (_hanging)
			{
			_hanging = false;
			GameObject.FindGameObjectWithTag("axe").GetComponent<SpringJoint2D>().enabled = false;
			GetComponent<Rigidbody2D>().drag = 0.5f;
			GetComponent<Animator>().SetBool("hanging",false);
			GetComponent<Rigidbody2D>().isKinematic = true;
			GetComponent<Rigidbody2D>().isKinematic = false;
			GetComponent<Rigidbody2D>().AddForce(new Vector2(800*transform.localScale.x,500));
			axeGraphic.enabled = true;
			doubleJump = false;
			}
		else if (!doubleJump && jerkTimer <= 0 && attack <= 0)
		{
			GetComponent<Rigidbody2D>().isKinematic = true;
			GetComponent<Rigidbody2D>().isKinematic = false;
			jumping = true;
			doubleJump = true;
			GameObject clone = Instantiate(doubleJumpEffect, transform.position, Quaternion.identity) as GameObject;
			Destroy(clone,1);
		}
	}

	public void JumpReleased()
	{
		pressedJump = false;
	}

	public void useAxe ()
	{
		pressedThrow = true;

		if (!onHardSurface && transformThrowAxe != null)
			if (!GameObject.FindGameObjectWithTag("axe"))
				{
				throwAxe = true;
				GetComponent<Rigidbody2D>().isKinematic = true;
				GetComponent<Rigidbody2D>().isKinematic = false;
				GetComponent<Rigidbody2D>().gravityScale = 0.1f;
				}
	}

	public void useAxeReleased ()
	{
		pressedThrow = false;
	}

	public void Attack ()
	{
		pressedAttack = true;

		if (!_hanging)
		{
		attack = 0.2f;
		GetComponent<Animator>().SetBool("attack", true);
		}
	}

	public void AttackReleased ()
	{
		pressedAttack = false;
	}

	public void useJerk ()
	{
		pressedJerk = true;

		if (jerkPower >= 1 && !_hanging)
		{
			if (onHardSurface)
				transform.Translate(new Vector3(0, 0.1f, 0));
			jerkPower -= 1;
			jerkTimer = 0.3f;
			GetComponent<Rigidbody2D>().isKinematic = true;
			GetComponent<Rigidbody2D>().isKinematic = false;

			// hide graphics
			SkinnedMeshRenderer[] graphics = GetComponentsInChildren<SkinnedMeshRenderer>();
			foreach (SkinnedMeshRenderer graphic in graphics)
				graphic.enabled = false;

			// swap motion blur
			if (transform.localScale.z == 0)
				jerkEffect.transform.Find("blur").GetComponent<ParticleSystemRenderer>().material.SetTexture("_MainTex", jerkTexture[0]);
			else
				jerkEffect.transform.Find("blur").GetComponent<ParticleSystemRenderer>().material.SetTexture("_MainTex", jerkTexture[1]);

			jerkEffect.SetActive(true);
		}
	}

	public void useJerkReleased ()
	{
		pressedJerk = false;
	}

	void OnTriggerEnter2D (Collider2D coll)
	{
		if (coll.gameObject.tag == "death")
		{
			health -= 100;
			hplineOffset -= 430;
		}

		if (coll.gameObject.tag == "death25" && lockdamageTimer <= 0)
		{
			lockdamageTimer = 0.5f;

			health -= 25;
			hplineOffset -= 110;

			deadforce = Vector2.Distance(transform.position, coll.transform.position);

			if (transform.position.x < coll.transform.position.x)
				deadforce *= -1;
		}
	}

}
