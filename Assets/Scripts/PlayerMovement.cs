using InControl;
using System.Linq;
using UnityEngine;

public class PlayerMovement : MonoBehaviour 
{

	public float moveSpeed = 5f;

	public InputDevice Device { get; set; }

	private Rigidbody2D rg;

	Vector2 movement;

	private void Start()
	{
		rg = GetComponent<Rigidbody2D>();
	}

	void Update () 
	{

		GetComponent<SpriteRenderer>().material.color = GetColorFromInput();
		
		movement.x = Device.Direction.X;
		movement.y = Device.Direction.Y;

		if (Device == null)
		{
			gameObject.SetActive(false);
		}
		else
		{
			gameObject.SetActive(true);

			rg.MovePosition(rg.position + movement * moveSpeed * Time.fixedDeltaTime);
		}
	}

	Color GetColorFromInput()
	{
		if (Device.Action1)
		{
			return Color.green;
		}

		if (Device.Action2)
		{
			return Color.red;
		}

		if (Device.Action3)
		{
			return Color.blue;
		}

		if (Device.Action4)
		{
			return Color.yellow;
		}

		return Color.white;
	}
}
