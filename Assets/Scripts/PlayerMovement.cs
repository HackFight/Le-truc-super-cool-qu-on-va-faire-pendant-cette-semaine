using InControl;
using System.Linq;
using UnityEngine;

public class PlayerMovement : MonoBehaviour 
{
	public int playerID;

	public InputDevice Device { get; set; }
	public GameObject nose;
	public Transform attach;
	public Transform front;

	public float moveSpeed = 20.0f;
	public float speedMalus = 0;

	public float dashSpeed = 60.0f;
	public float dashTime = 1.0f;
	public float dashCoolDown = 5.0f;

	private Rigidbody2D rg;
	private Egg eggScript;
	private GameManager gameManager;
	private Vector2 movement;
	private bool haveEggInHands;
	private bool isLeftTriggerPressed;
	private bool canDash = true;
	private bool isDashing = false;
	private bool canTurn = true;
	private bool isMonster;

	private void Start()
	{
		eggScript = FindObjectOfType<Egg>();
		rg = GetComponent<Rigidbody2D>();
		gameManager = FindObjectOfType<GameManager>();

		canDash = true;

		if(playerID == 0)
		{
			nose.GetComponent<SpriteRenderer>().color = Color.blue;
		}
		else if (playerID == 1)
		{
			nose.GetComponent<SpriteRenderer>().color = Color.red;
		}
		else if (playerID == 2)
		{
			nose.GetComponent<SpriteRenderer>().color = Color.yellow;
		}
		else if (playerID == 3)
		{
			nose.GetComponent<SpriteRenderer>().color = Color.green;
		}
	}

	void Update()
	{
		if (isDashing == false)
		{
			movement.x = Device.Direction.X;
			movement.y = Device.Direction.Y;
		}

		MoveEgg();


		if (Device.RightTrigger.WasPressed)
		{
			if (canDash && !haveEggInHands)
			{
				canDash = false;
				canTurn = false;
				Dash();
				Invoke("SetDashToTrue", dashCoolDown);
				Invoke("SetIsDashingToFalse", dashTime);
			}
		}

		if (Device.LeftTrigger.WasPressed && haveEggInHands == true)
		{
			Drop();
			isLeftTriggerPressed = false;
		}
		else
		{
			if (Device.LeftTrigger.WasPressed)
			{
				isLeftTriggerPressed = true;
			}
			else if (Device.LeftTrigger.WasReleased)
			{
				isLeftTriggerPressed = false;
			}
		}
		
		if(((Vector2)Device.Direction).magnitude > 0.5f)
		{
			front.position = transform.position + Device.Direction;
		}

		Vector2 lookDirection = front.position - transform.position;
		float lookAngle = Vector2.SignedAngle(Vector2.right, lookDirection);

		if (canTurn)
		{
			transform.rotation = Quaternion.Euler(0, 0, lookAngle);
		}
	}

	private void FixedUpdate()
	{
		if (Device == null)
		{
			gameObject.SetActive(false);
		}
		else
		{
			gameObject.SetActive(true);

			if (haveEggInHands == false && isDashing == false)
			{
				rg.MovePosition(rg.position + movement * moveSpeed);
			}
			else if (haveEggInHands == true && isDashing == false)
			{
				rg.MovePosition(rg.position + movement * (moveSpeed - speedMalus));
			}
			else if (haveEggInHands == false && isDashing == true)
			{
				rg.MovePosition(rg.position + movement * dashSpeed);
			} 
		}
	}

	private void Grab()
	{
		if (eggScript.isGrabed == false)
		{
			eggScript.isGrabed = true;
			haveEggInHands = true;
		}
	}

	public void Drop()
	{

		if (eggScript.isGrabed == true)
		{
			eggScript.isGrabed = false;
			haveEggInHands = false;
		}
	}

	private void MoveEgg()
	{
		if (haveEggInHands == true)
		{
			eggScript.transform.position = attach.position;
		}
	}

	private void Dash()
	{
		isDashing = true;
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.CompareTag("Egg") && isLeftTriggerPressed && eggScript.isGrabed == false)
		{	
			Grab();
		}

		if (collision.CompareTag("Player") && collision.GetComponent<PlayerMovement>().haveEggInHands && isDashing)
		{
			collision.GetComponent<PlayerMovement>().canDash = false;
			collision.GetComponent<PlayerMovement>().Invoke("SetDashToTrue", dashCoolDown);
			collision.GetComponent<PlayerMovement>().Drop();
			Grab();
		}
	}

	private void SetDashToTrue()
	{
		canDash = true;
	}

	private void SetIsDashingToFalse()
	{
		isDashing = false;
		canTurn = true;
	}

	private void TurnIntoMonsters()
	{
		GetComponentInChildren<SpriteRenderer>().color = Color.red;
	}
}
