using InControl;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour 
{
	public int playerID;

	public InputDevice Device { get; set; }
	public GameObject nose;
	public Transform attach;
	public Transform bulletSpawnPoint;
	public Transform front;
	public GameObject view;
	public GameObject shockZone;
	public TextMeshProUGUI lifesText;
	public GameObject bullet;

	public float moveSpeed = 0.1f;
	public float speedMalus = 0;

	public float dashSpeed = 1.0f;
	public float dashTime = 0.1f;
	public float dashCoolDown = 5.0f;

	public float bulletSpeed = 2.0f;

	public int playerLifes;
	public int playerMaxLifes = 3;
	public int monsterLifes;
	public int monsterMaxLifes = 5;

	private Rigidbody2D rg;
	private Egg eggScript;
	private GameManager gameManager;
	private Vector2 movement;

	private bool haveEggInHands = false;
	private bool isLeftTriggerPressed = false;
	private bool canDash = true;
	private bool isDashing = false;
	private bool canTurn = true;
	private bool isMonster = false;
	private bool shock = false;
	private bool isDead = false;
	private bool canShoot = true;

	private void Start()
	{
		eggScript = FindObjectOfType<Egg>();
		rg = GetComponent<Rigidbody2D>();
		gameManager = FindObjectOfType<GameManager>();

		canDash = true;

		playerLifes = playerMaxLifes;
		monsterLifes = monsterMaxLifes;

		shockZone.SetActive(false);

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
		if (!isDead)
		{
			if (playerLifes <= 0)
			{
				Die();
			}

			if (isDashing == false)
			{
				movement.x = Device.Direction.X;
				movement.y = Device.Direction.Y;
			}

			MoveEgg();

			if (shock)
			{
				shock = false;
				shockZone.SetActive(false);
			}

			if (Device.RightTrigger.WasPressed)
			{
				if (isMonster)
				{
					Shoot();
				}
				else
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
			}


			if (Device.LeftTrigger.WasPressed)
			{

				if (isMonster)
				{

					Shock();

				}
				else
				{
					if (haveEggInHands == true)
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
			}

			if (((Vector2)Device.Direction).magnitude > 0.5f)
			{
				front.position = transform.position + Device.Direction;
			}

			Vector2 lookDirection = front.position - transform.position;
			float lookAngle = Vector2.SignedAngle(Vector2.right, lookDirection);

			if (canTurn)
			{
				transform.rotation = Quaternion.Euler(0, 0, lookAngle);
			}

			if (isMonster == false && haveEggInHands && gameManager.chrono >= gameManager.timeToOpenEgg)
			{
				eggScript.gameObject.SetActive(false);
				TurnIntoMonster();
			}

			lifesText.text = playerLifes.ToString();
		}
	}

	private void FixedUpdate()
	{
		if (!isDead)
		{
			if (isMonster == false)
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
						rg.MovePosition(rg.position + (Vector2)(front.position - transform.position) * dashSpeed);
					}
				}
			}
		}
	}

	private void Grab()
	{
		if (eggScript.isGrabed == false && eggScript.canBeGrabed == true)
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

	private void Shock()
	{
		Invoke("SetShockToTrue", 0.5f);
		shockZone.SetActive(true);
	}

	private void SetShockToTrue()
	{
		shock = true;
	}

	private void Shoot()
	{
		GameObject temporaryBullet = Instantiate(bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
		temporaryBullet.GetComponent<Rigidbody2D>().AddForce((front.position - transform.position) * bulletSpeed);
	}

	private void TurnIntoMonster()
	{
		eggScript.canBeGrabed = false;
		isMonster = true;
		Drop();
		view.GetComponent<SpriteRenderer>().color = Color.red;
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		if (!isDead)
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
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Bullet"))
		{

			playerLifes--;
			Destroy(collision.gameObject);

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

	private void Die()
	{
		isDead = true;
		view.GetComponent<SpriteRenderer>().color = Color.grey;
	}
}
