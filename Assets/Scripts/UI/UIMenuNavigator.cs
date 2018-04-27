using UnityEngine;

public class UIMenuNavigator : MonoBehaviour
{
	private static UIMenuNavigator Instance;

	[SerializeField]
	private RectTransform _mainMenuUIRoot;
	[SerializeField]
	private RectTransform _inGameUIRoot;
	[SerializeField]
	private RectTransform _postGameUIRoot;

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

		SwitchToMainMenuUI();
	}

	public static void SwitchToMainMenuUI()
	{
		Instance._inGameUIRoot.gameObject.SetActive(false);
		Instance._postGameUIRoot.gameObject.SetActive(false);
		Instance._mainMenuUIRoot.gameObject.SetActive(true);
	}

	public static void SwitchToInGameUI()
	{
		Instance._mainMenuUIRoot.gameObject.SetActive(false);
		Instance._postGameUIRoot.gameObject.SetActive(false);
		Instance._inGameUIRoot.gameObject.SetActive(true);
	}

	public static void SwitchToPostGameUI()
	{
		Instance._mainMenuUIRoot.gameObject.SetActive(false);
		Instance._inGameUIRoot.gameObject.SetActive(false);
		Instance._postGameUIRoot.gameObject.SetActive(true);
	}

	public static void DisableAllUI()
	{
		Instance._inGameUIRoot.gameObject.SetActive(false);
		Instance._mainMenuUIRoot.gameObject.SetActive(false);
	}
}
