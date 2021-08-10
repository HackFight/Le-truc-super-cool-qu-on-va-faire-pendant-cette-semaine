using InControl;
using System.Linq;
using UnityEngine;

public class PlayerMovement : MonoBehaviour 
{
	public int playerID;

	public InputDevice Device { get; set; }
	public GameObject nose;
	public Transform attach;

	public float moveSpeed = 20.0f;
	public float speedMalus = 5.0f;

	public float dashSpeed = 60.0f;
	public float dashTime = 1.0f;
	public float dashCoolDown = 5.0f;

	private Rigidbody2D rg;
	private Egg eggScript;
	Vector2 movement;
	private bool haveEggInHands;
	private bool isLeftTriggerPressed;
	private bool canDash = true;
	private bool isDashing = false;

	private void Start()
	{
		eggScript = FindObjectOfType<Egg>();
		rg = GetComponent<Rigidbody2D>();

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

		movement.x = Device.Direction.X;
		movement.y = Device.Direction.Y;

		MoveEgg();

		if (Device == null)
		{
			gameObject.SetActive(false);
		}
		else
		{
			gameObject.SetActive(true);

			if (haveEggInHands == false && isDashing == false)
			{
				rg.MovePosition(rg.position + movement * moveSpeed * Time.deltaTime);
			}
			else if(haveEggInHands == true && isDashing == false)
			{
				rg.MovePosition(rg.position + movement * (moveSpeed - speedMalus) * Time.deltaTime);
			}
			else if(haveEggInHands == false && isDashing == true)
			{
				rg.MovePosition(rg.position + movement *  dashSpeed * Time.deltaTime);
			}


		}

		if (Device.RightTrigger.WasPressed)
		{
			if (canDash && !haveEggInHands)
			{
				canDash = false;
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
	}

	private void Grab()
	{
		if (eggScript.isGrabed == false)
		{
			eggScript.isGrabed = true;
			haveEggInHands = true;
		}
	}

	private void Drop()
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
		Debug.Log("Dash!");

	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.CompareTag("Egg") && isLeftTriggerPressed && eggScript.isGrabed == false)
		{	
			Grab();
		}
	}

	void SetDashToTrue()
	{
		canDash = true;
	}

	void SetIsDashingToFalse()
	{
		isDashing = false;
	}
}
