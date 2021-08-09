using UnityEngine;

public class PlayerMovement : MonoBehaviour 
{

	public float moveSpeed = 5f;

	private Rigidbody2D rg;

	Vector2 movement;

	private void Start()
	{
		rg = GetComponent<Rigidbody2D>();
	}

	void Update () 
	{
		movement.x = Input.GetAxisRaw("Horizontal");
		movement.y = Input.GetAxisRaw("Vertical");
	}

	void FixedUpdate()
	{
		rg.MovePosition(rg.position + movement * moveSpeed * Time.fixedDeltaTime);
	}
}
