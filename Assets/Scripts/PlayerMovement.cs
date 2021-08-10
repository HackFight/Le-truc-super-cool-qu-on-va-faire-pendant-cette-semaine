using InControl;
using System.Linq;
using UnityEngine;

public class PlayerMovement : MonoBehaviour 
{

	public float moveSpeed = 5f;
	public InputDevice Device { get; set; }
	public GameObject nose;
	private Rigidbody2D rg;
	Vector2 movement;
	public Egg eggScript;
	private bool haveEggInHands;
	public Transform attach;
	private bool isLeftTriggerPressed;
	public int playerID;
	public float speedMalus;

	private void Start()
	{
		eggScript = FindObjectOfType<Egg>();
		rg = GetComponent<Rigidbody2D>();

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

	void Update () 
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

			if(haveEggInHands == false)
			{
				rg.MovePosition(rg.position + movement * moveSpeed * Time.deltaTime);
			}
			else
			{
				rg.MovePosition(rg.position + movement * (moveSpeed - speedMalus) * Time.deltaTime);
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

			Debug.Log("Grabed Egg");
		}
	}

	private void Drop()
	{
		if (eggScript.isGrabed == true)
		{
			eggScript.isGrabed = false;
			haveEggInHands = false;

			Debug.Log("Dropped Egg");
		}
	}

	private void MoveEgg()
	{
		if (haveEggInHands == true)
		{
			eggScript.transform.position = attach.position;
		}
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.CompareTag("Egg") && isLeftTriggerPressed && eggScript.isGrabed == false)
		{	
			Grab();
		}
	}
}
