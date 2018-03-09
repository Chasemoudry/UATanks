using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class GameManager : MonoBehaviour
{
	private static GameManager Instance { get; set; }

	private event System.Action Event_GameOver;

	[SerializeField]
	private int playerScore;

	private List<IVehicle> playerList = new List<IVehicle>();
	private List<IVehicle> enemyList = new List<IVehicle>();

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else
		{
			Destroy(this);
		}
	}

	private void Start()
	{
		// TODO: Player Death
		Instance.Event_GameOver += () => { Debug.LogWarning("Player Has Died!"); };

		// TODO: Spawn sequence
		SpawnPlayerVehicle("Ship_Basic_Player", Vector3.up, Quaternion.identity);
	}

	/// <summary>
	/// Increments the player's score by an amount.
	/// </summary>
	/// <param name="amount">The amount of points being added to the current score.</param>
	public static void IncrementPlayerScore(int amount)
	{
		Instance.playerScore += amount;
#if UNITY_EDITOR
		Debug.Log("Player Score is now = " + Instance.playerScore);
#endif
	}

	public static void SpawnPlayerVehicle(string prefabAssetPath, Vector3 spawnPosition, Quaternion spawnRotation)
	{
		// OPTION: Instantiate from object pool
		GameObject newObject = Instantiate(Resources.Load<GameObject>(prefabAssetPath), spawnPosition, spawnRotation);
		IVehicle vehicleComponent = newObject.GetComponent<IVehicle>();

		if (vehicleComponent == null)
		{
			Debug.LogError("Requested player asset has no IVehicle component!");
			Destroy(newObject);
		}
		else
		{
			Instance.playerList.Add(vehicleComponent);
			Camera_FollowObject.ObjectToFollow = newObject.transform;
		}
	}

	public static void DespawnPlayerVehicle(GameObject gameObject)
	{
		if (gameObject != null)
		{
			Instance.playerList.Remove(gameObject.GetComponent<IVehicle>());
			// OPTION: Remove from object pool
			Destroy(gameObject);
		}
	}

	public static void SpawnAIVehicle(string prefabAssetPath, Vector3 spawnPosition, Quaternion spawnRotation)
	{
		// OPTION: Instantiate from object pool
		GameObject newObject = Instantiate(Resources.Load<GameObject>(prefabAssetPath), spawnPosition, spawnRotation);
		IVehicle vehicleComponent = newObject.GetComponent<IVehicle>();

		if (vehicleComponent == null)
		{
			Debug.LogError("Requested AI asset has no IVehicle component!");
			Destroy(newObject);
		}
		else
		{
			Instance.enemyList.Add(vehicleComponent);
		}
	}

	public static void DespawnAIVehicle(GameObject gameObject)
	{
		if (gameObject != null)
		{
			Instance.enemyList.Remove(gameObject.GetComponent<IVehicle>());
			// OPTION: Remove from object pool
			Destroy(gameObject);
		}
	}
}
