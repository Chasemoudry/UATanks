using UnityEngine;

/// <summary>
/// Cinemachine-driven camera system for player objects.
/// </summary>
[DisallowMultipleComponent]
public class CameraSystem : MonoBehaviour
{
	// TODO: Update to multiplayer-compatible system (enum? spawn?)
	private static CameraSystem Instance;

	[SerializeField]
	private Camera playerOneCamera;
	[SerializeField]
	private Cinemachine.CinemachineClearShot clearShot1;

	/// <summary>The object being followed by the ClearShot camera.</summary>
	public static Transform ObjectToFollow
	{
		get
		{
			return objectToFollow;
		}
		set
		{
			objectToFollow = value;
			// OnValueChanged: Update ClearShot variables to equal new value
			Instance.clearShot1.Follow = objectToFollow;
			Instance.clearShot1.LookAt = objectToFollow;
		}
	}

	/// <summary>
	/// Private field for public ObjectToFollow property.
	/// </summary>
	private static Transform objectToFollow;

	private void Awake()
	{
		// Singleton sequence
		if (Instance == null)
		{
			Instance = this;
		}
		else
		{
			Destroy(this);
		}
	}

	public static void DisableCamera()
	{
		Instance.playerOneCamera.enabled = false;
	}

	public static void EnableCamera()
	{
		Instance.playerOneCamera.enabled = true;
	}
}
