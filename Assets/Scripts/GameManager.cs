using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[DisallowMultipleComponent]
public class GameManager : MonoBehaviour
{
	public static GameObject PlayerOne { get { return Instance.playerList[0]; } }

	public MapGeneration.Map.MapVector2 mapSize;
	public bool MapOfTheDay = false;

	//public GameObject camSystem_PlayerOne;
	//public GameObject camSystem_PlayerTwo;

	private static GameManager Instance { get; set; }

	private event System.Action Event_GameOver;
	private MapGeneration.Map mapInstance;

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


		BeginGame();
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.R))
		{
			RestartGame();
		}
	}

	private static void BeginGame()
	{
		CameraSystem.DisableCamera();

		if (Instance.MapOfTheDay)
		{
			Random.InitState(System.Int32.Parse(
				System.DateTime.Now.Year.ToString() +
				System.DateTime.Now.Month.ToString() +
				System.DateTime.Now.Day.ToString()));
		}
		else
		{
			Random.InitState((int)System.DateTime.Now.Ticks);
		}

		Instance.mapInstance = new GameObject().AddComponent<MapGeneration.Map>();
		Instance.mapInstance.name = "Map Instance";
		Instance.mapInstance.MapSize = Instance.mapSize;
	}

	private static void EndGame()
	{
		Destroy(Instance.mapInstance.gameObject);
		Instance.playerList.Clear();
		Instance.enemyList.Clear();
	}

	private static void RestartGame()
	{
		Destroy(Instance.mapInstance.gameObject);

		for (int i = Instance.playerList.Count - 1; i >= 0; i--)
		{
			Destroy(Instance.playerList[i]);
		}

		Instance.enemyList.Clear();
		Instance.playerList.Clear();

		BeginGame();
	}

	public static void RebuildNavMesh()
	{
		Instance.GetComponent<NavMeshSurface>().BuildNavMesh();
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

	public static void SpawnPlayerVehicle(Vector3 spawnPosition, Quaternion spawnRotation)
	{
		// OPTION: Instantiate from object pool
		GameObject newObject = Instantiate(Resources.Load<GameObject>("Ship_Player"),
			spawnPosition, spawnRotation);

		if (newObject.GetComponent<IVehicle>() == null)
		{
			Debug.LogError("Requested player asset has no IVehicle component!");
			Destroy(newObject);
		}
		else
		{
			Instance.playerList.Add(newObject);
			CameraSystem.ObjectToFollow = newObject.transform;
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

	public static GameObject SpawnAIVehicle(string prefabAssetPath, Vector3 spawnPosition,
		Quaternion spawnRotation, Transform[] waypoints)
	{
		// OPTION: Instantiate from object pool
		GameObject newObject = Instantiate(Resources.Load<GameObject>(prefabAssetPath),
			spawnPosition, spawnRotation);

		if (waypoints.Length > 0)
		{
			newObject.GetComponent<INavigator>().SetWaypoints(waypoints);
		}

		Instance.enemyList.Add(newObject);

		return newObject;
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
