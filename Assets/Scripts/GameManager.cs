using System;
using System.Collections.Generic;
using MapGeneration;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Random = UnityEngine.Random;

[DisallowMultipleComponent]
public class GameManager : MonoBehaviour
{
	public static GameObject PlayerOne
	{
		get
		{
			if (Instance == null)
			{
				return null;
			}

			return Instance._playerList[0];
		}
	}

	public static int PlayerScore { get { return Instance._playerScore; } }

	private static GameManager Instance { get; set; }

	private event Action GameOver;

	[SerializeField]
	private Dropdown _mapSizeXDropdown;
	[SerializeField]
	private Dropdown _mapSizeYDropdown;
	[SerializeField]
	private Toggle _mapOfTheDayToggle;

	private Map _mapInstance;
	private int _playerScore;
	private List<GameObject> _playerList = new List<GameObject>();
	private List<GameObject> _enemyList = new List<GameObject>();

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

		this._playerList = new List<GameObject>();
		this._enemyList = new List<GameObject>();
	}

	private void Start()
	{
		// TODO: Player Death
		Instance.GameOver +=
			() =>
			{
				Debug.LogWarning("Player Has Died!");
				EndGame();
			};
	}

	public void ExitToDesktop()
	{
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#else
		Application.Quit();
#endif
	}

	public void BeginGame()
	{
		// Main camera and UI is turned back on after the map is generated
		CustomCamera.CameraSystem.DisableCamera();
		UIMenuNavigator.DisableAllUI();

		Instance._playerScore = 0;

		if (Instance._mapOfTheDayToggle.isOn)
		{
			Random.InitState(Int32.Parse(
				DateTime.Now.Year +
				DateTime.Now.Month.ToString() +
				DateTime.Now.Day));
		}
		else
		{
			Random.InitState((int)DateTime.Now.Ticks);
		}

		Instance._mapInstance = new GameObject().AddComponent<Map>();
		Instance._mapInstance.name = "Map Instance";
		Instance._mapInstance.MapSize = new Map.MapVector2(
			Instance._mapSizeXDropdown.value + 1, Instance._mapSizeYDropdown.value + 1);
	}

	public static void EndGame()
	{
		Destroy(Instance._mapInstance.gameObject);

		for (int i = Instance._playerList.Count - 1; i >= 0; i--)
		{
			Destroy(Instance._playerList[i]);
		}

		Instance._playerList.Clear();
		Instance._enemyList.Clear();

		UIMenuNavigator.SwitchToPostGameUI();
		StatsWindow.UpdateStats();
	}

	public void RestartGame()
	{
		Destroy(Instance._mapInstance.gameObject);

		for (int i = Instance._playerList.Count - 1; i >= 0; i--)
		{
			Destroy(Instance._playerList[i]);
		}

		Instance._enemyList.Clear();
		Instance._playerList.Clear();

		Instance.BeginGame();
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
		Instance._playerScore += amount;

#if UNITY_EDITOR
		Debug.Log("Player Score is now = " + Instance._playerScore);
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
			Instance._playerList.Add(newObject);
			CustomCamera.CameraSystem.ObjectToFollow = newObject.transform;
		}
	}

	public static void DespawnPlayerVehicle(GameObject gameObject)
	{
		if (gameObject == null)
		{
			return;
		}

		Instance._playerList.Remove(gameObject);
		// OPTION: Remove from object pool
		Destroy(gameObject);
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

		Instance._enemyList.Add(newObject);

		return newObject;
	}

	public static void DespawnAIVehicle(GameObject gameObject)
	{
		if (gameObject == null)
		{
			return;
		}

		Instance._enemyList.Remove(gameObject);
		// OPTION: Remove from object pool
		Destroy(gameObject);

		if (Instance._enemyList.Count == 0)
		{
			EndGame();
		}
	}

	public static void RaiseGameOver()
	{
		if (Instance.GameOver != null)
		{
			Instance.GameOver();
		}
	}
}