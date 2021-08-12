using System.Collections.Generic;
using InControl;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
	public GameObject playerPrefab;

	const int maxPlayers = 4;

	List<Vector2> playerPositions = new List<Vector2>() 
	{
			new Vector2( -1, 1),
			new Vector2( 1, 1),
			new Vector2( -1, -1),
			new Vector2( 1, -1),
	};

	List<PlayerMovement> players = new List<PlayerMovement>(maxPlayers);



	void Start()
	{
		InputManager.OnDeviceDetached += OnDeviceDetached;
	}


	void Update()
	{
		var inputDevice = InputManager.ActiveDevice;

		if (JoinButtonWasPressedOnDevice(inputDevice))
		{
			if (ThereIsNoPlayerUsingDevice(inputDevice))
			{
				CreatePlayer(inputDevice);
			}
		}
	}


	bool JoinButtonWasPressedOnDevice(InputDevice inputDevice)
	{
		return inputDevice.Action1.WasPressed || inputDevice.Action2.WasPressed || inputDevice.Action3.WasPressed || inputDevice.Action4.WasPressed;
	}


	PlayerMovement FindPlayerUsingDevice(InputDevice inputDevice)
	{
		var playerCount = players.Count;
		for (var i = 0; i < playerCount; i++)
		{
			var player = players[i];
			if (player.Device == inputDevice)
			{
				return player;
			}
		}

		return null;
	}


	bool ThereIsNoPlayerUsingDevice(InputDevice inputDevice)
	{
		return FindPlayerUsingDevice(inputDevice) == null;
	}


	void OnDeviceDetached(InputDevice inputDevice)
	{
		var player = FindPlayerUsingDevice(inputDevice);
		if (player != null)
		{
			RemovePlayer(player);
		}
	}


	PlayerMovement CreatePlayer(InputDevice inputDevice)
	{
		if (players.Count < maxPlayers)
		{
			// Pop a position off the list. We'll add it back if the player is removed.
			var playerPosition = playerPositions[0];
			playerPositions.RemoveAt(0);

			var gameObject = (GameObject)Instantiate(playerPrefab, playerPosition, Quaternion.identity);
			var player = gameObject.GetComponent<PlayerMovement>();
			player.Device = inputDevice;
			player.playerID = players.Count;
			players.Add(player);

			return player;
		}

		return null;
	}


	void RemovePlayer(PlayerMovement player)
	{
		playerPositions.Insert(0, player.transform.position);
		players.Remove(player);
		player.Device = null;
		Destroy(player.gameObject);
	}
}
