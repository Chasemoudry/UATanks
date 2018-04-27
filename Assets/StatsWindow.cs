using UnityEngine;
using UnityEngine.UI;

public class StatsWindow : MonoBehaviour
{
	private static StatsWindow Instance;

	[SerializeField]
	private Text m_ScoreText;
	[SerializeField]
	private Button m_ReturnButton;

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

		this.m_ReturnButton.onClick.AddListener(UIMenuNavigator.SwitchToMainMenuUI);
	}

	public static void UpdateStats()
	{
		Instance.m_ScoreText.text = GameManager.PlayerScore.ToString();
	}
}
