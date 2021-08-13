using InControl;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

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
	public GameObject monsterFrontPrefab;
	private GameObject monsterFront;
	public GameObject turningStuff;

	public float moveSpeed = 0.1f;
	public float speedMalus = 0;
	public float monsterTurnSpeed = 0.5f;
	public float turnSpeed = 1000f;

	public float dashSpeed = 1.0f;
	public float dashTime = 0.1f;
	public float dashCoolDown = 5.0f;

	public float shockCooldown = 5.0f;
	public float shootCooldown = 0.1f;

	public float bulletSpeed = 2.0f;

	public int playerLifes;
	public int playerMaxLifes = 3;
	public int monsterMaxLifes = 5;

	public float immortalityTime = 1.0f;
	public float monsterImmortalityTime = 0.1f;

	private Rigidbody rg;
	private Egg eggScript;
	private GameManager gameManager;
	private SoundManager soundManager;
	private Vector3 movement;

	public bool haveEggInHands = false;
	public bool isLeftTriggerPressed = false;
	public bool canDash = true;
	public bool isDashing = false;
	public bool canTurn = true;
	public bool isMonster = false;
	public bool shock = false;
	public bool isDead = false;
	public bool canShock = true;
	public bool canShoot = true;
	public bool immortal = false;

	private void Start()
	{
		eggScript = FindObjectOfType<Egg>();
		rg = GetComponent<Rigidbody>();
		gameManager = FindObjectOfType<GameManager>();
		soundManager = FindObjectOfType<SoundManager>();

		view.GetComponent<MeshRenderer>().material.color = Color.white;

		monsterFront = Instantiate(monsterFrontPrefab, front.position, front.rotation);

		canDash = true;

		playerLifes = playerMaxLifes;

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
			
			if (((Vector3)Device.Direction).magnitude > 0.5f)
			{
				monsterFront.transform.position = front.transform.position;
			}

			if (isDashing == false)
			{
				movement.x = Device.Direction.X;
				movement.z = Device.Direction.Y;
			}

			MoveEgg();

			if (shock)
			{
				shock = false;
				shockZone.SetActive(false);
			}

			if (Device.RightTrigger.WasPressed)
			{
				if (isMonster == false)
				{
					if (canDash && !haveEggInHands)
					{
						canDash = false;
						canTurn = false;
						soundManager.PlayDash();
						Dash();
						Invoke("SetDashToTrue", dashCoolDown);
						Invoke("SetIsDashingToFalse", dashTime);
					}
				}
			}

			if (Device.RightTrigger.IsPressed && isMonster)
			{
				Shoot();
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

			if (((Vector3)Device.Direction).magnitude > 0.5f)
			{
				if (canTurn &&  !isMonster)
				{
					front.position = transform.position + movement;
				}
				else if(isMonster)
				{
					monsterFront.transform.position = transform.position + movement;
				}
			}

			if (isMonster == false && haveEggInHands && gameManager.chrono >= gameManager.timeToOpenEgg)
			{
				eggScript.gameObject.SetActive(false);
				TurnIntoMonster();
			}


			if (isMonster == false)
			{

				if (canTurn)
				{
					Vector3 dir = front.transform.position - transform.position;
					var dirCross = Vector3.Cross(dir, Vector3.up);
					Quaternion lookRotation = Quaternion.LookRotation(dirCross, Vector3.up);
					Vector3 rotation = Quaternion.Lerp(turningStuff.transform.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
					turningStuff.transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
				}
			}
			else
			{
				Vector3 dir = monsterFront.transform.position - transform.position;
				var dirCross = Vector3.Cross(dir, Vector3.up);
				Quaternion lookRotation = Quaternion.LookRotation(dirCross, Vector3.up);
				Vector3 rotation = Quaternion.Lerp(turningStuff.transform.rotation, lookRotation, Time.deltaTime * monsterTurnSpeed).eulerAngles;
				turningStuff.transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
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
						rg.MovePosition(rg.position + (Vector3)(front.position - transform.position) * dashSpeed);
					}
				}

				if((movement.x >= 0.1 || movement.z >= 0.1 || movement.x <= -0.1 || movement.z <= -0.1) && soundManager.walkcycle_run.isPlaying == false)
                {
					soundManager.walkcycle_run.Play();
                }

				if((movement.x < 0.1 && movement.z < 0.1 && movement.x > -0.1 && movement.z > -0.1) && soundManager.walkcycle_run.isPlaying == true){
					soundManager.walkcycle_run.Stop();
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
		if (canShock)
		{
			canShock = false;
			Invoke("SetCanShockToTrue", shockCooldown);
			Invoke("SetShockToTrue", 0.5f);
			shockZone.SetActive(true);
		}
	}

	private void SetShockToTrue()
	{
		shock = true;
	}

	private void Shoot()
	{
		if (canShoot)
		{
			canShoot = false;

			soundManager.PlayShoot();

			GameObject temporaryBullet = Instantiate(bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
			temporaryBullet.GetComponent<Rigidbody>().AddForce((bulletSpawnPoint.position - transform.position) * bulletSpeed);
			Invoke("SetCanShootToTrue", shootCooldown);
		}
	}

	private void TurnIntoMonster()
	{
		eggScript.canBeGrabed = false;
		isMonster = true;

		playerLifes = monsterMaxLifes;

		Drop();

		view.GetComponent<MeshRenderer>().material.color = Color.red;
	}

	private void OnTriggerStay(Collider collision)
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

	private void OnTriggerEnter(Collider collision)
	{
		if (!isDead)
		{
			if (!isMonster)
			{
				if (collision.CompareTag("Bullet") && immortal == false && playerLifes > 0)
				{
					immortal = true;
					Invoke("SetImmortalToFalse", immortalityTime);
					view.GetComponent<MeshRenderer>().material.color = Color.green;

					playerLifes--;

					Destroy(collision.gameObject);
				}
			}
			else
			{
				if(collision.CompareTag("Player") && collision.GetComponent<PlayerMovement>().isDashing && immortal == false)
				{
					immortal = true;
					view.GetComponent<MeshRenderer>().material.color = Color.green;
					Invoke("SetImmortalToFalse", monsterImmortalityTime);
					playerLifes--;
				}
			}
		}
	}

	private void SetDashToTrue()
	{
		canDash = true;
	}

	private void SetCanShockToTrue()
	{
		canShock= true;
	}

	private void SetCanShootToTrue()
	{
		canShoot = true;
	}

	private void SetIsDashingToFalse()
	{
		isDashing = false;
		canTurn = true;
	}

	private void SetImmortalToFalse()
	{
		view.GetComponent<MeshRenderer>().material.color = Color.white;
		immortal = false;
	}

	private void Die()
	{
		isDead = true;
		view.GetComponent<MeshRenderer>().material.color = Color.grey;
	}
}
