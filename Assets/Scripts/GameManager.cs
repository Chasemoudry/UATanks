using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class GameManager : MonoBehaviour
{
	private static GameManager Instance { get; set; }

	private event System.Action Event_GameOver;

	public static GameObject PlayerOne { get { return Instance.playerList[0]; } }

#if DEBUG
	[Header("DEBUG")]
	[SerializeField]
#endif
	private int playerScore;

#if DEBUG
	[SerializeField]
#endif
	private List<GameObject> playerList = new List<GameObject>();

#if DEBUG
	[SerializeField]
#endif
	private List<GameObject> enemyList = new List<GameObject>();

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

	public static void SpawnPlayerVehicle(string resourceFilePath, Vector3 spawnPosition, Quaternion spawnRotation)
	{
		// OPTION: Instantiate from object pool
		GameObject newObject = Instantiate(Resources.Load<GameObject>(resourceFilePath), spawnPosition, spawnRotation);

		if (newObject.GetComponent<IVehicle>() == null)
		{
			Debug.LogError("Requested player asset has no IVehicle component!");
			Destroy(newObject);
		}
		else
		{
			Instance.playerList.Add(newObject);
			Camera_FollowObject.ObjectToFollow = newObject.transform;
		}
	}

	public static void DespawnPlayerVehicle(GameObject gameObject)
	{
		if (gameObject != null)
		{
			Instance.playerList.Remove(gameObject);
			// OPTION: Remove from object pool
			Destroy(gameObject);
		}
	}

	public static void SpawnAIVehicle(string prefabAssetPath, Vector3 spawnPosition, Quaternion spawnRotation)
	{
		// OPTION: Instantiate from object pool
		GameObject newObject = Instantiate(Resources.Load<GameObject>(prefabAssetPath), spawnPosition, spawnRotation);

		if (newObject.GetComponent<IVehicle>() == null)
		{
			Debug.LogError("Requested AI asset has no IVehicle component!");
			Destroy(newObject);
		}
		else
		{
			Instance.enemyList.Add(newObject);
		}
	}

	public static void SpawnAIVehicle(string prefabAssetPath, Vector3 spawnPosition, Quaternion spawnRotation,
		Transform[] waypoints)
	{
		// OPTION: Instantiate from object pool
		GameObject newObject = Instantiate(Resources.Load<GameObject>(prefabAssetPath), spawnPosition, spawnRotation);

		if (newObject.GetComponent<IVehicle>() == null)
		{
			Debug.LogError("Requested AI asset has no IVehicle component!");
			Destroy(newObject);
		}
		else
		{
			newObject.GetComponent<INavigator>().SetWaypoints(waypoints);
			Instance.enemyList.Add(newObject);
		}
	}

	public static void DespawnAIVehicle(GameObject gameObject)
	{
		if (gameObject != null)
		{
			Instance.enemyList.Remove(gameObject);
			// OPTION: Remove from object pool
			Destroy(gameObject);
		}
	}
}
