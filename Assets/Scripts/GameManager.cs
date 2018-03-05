using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance { get; private set; }

	public int PlayerScore { get { return Instance.playerScore; } }

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

	public void IncrementPlayerScore(int amount)
	{
		Instance.playerScore += amount;
	}
}
