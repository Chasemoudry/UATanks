using UnityEngine;

[DisallowMultipleComponent]
public class GameManager : MonoBehaviour
{
	public static GameManager Instance { get; private set; }

	public event System.Action OnPlayerDeath;

	public int PlayerScore { get { return Instance.playerScore; } }

	// REMOVE
	[SerializeField]
	private int playerScore;

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(Instance);
		}
		else
		{
			Destroy(this);
		}
	}

	private void Start()
	{
		// TODO: Player Death
		Instance.OnPlayerDeath += () => { Debug.LogWarning("Player Has Died!"); };
	}

	public void IncrementPlayerScore(int amount)
	{
		Instance.playerScore += amount;
		Debug.Log("Player Score is now = " + Instance.playerScore);
	}

	public static void KillPlayer()
	{
		Instance.OnPlayerDeath();
	}
}
