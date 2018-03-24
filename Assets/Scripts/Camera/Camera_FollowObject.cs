using UnityEngine;

/// <summary>
/// Cinemachine-driven camera system for player objects.
/// </summary>
[DisallowMultipleComponent, RequireComponent(typeof(Cinemachine.CinemachineClearShot))]
public class Camera_FollowObject : MonoBehaviour
{
	// TODO: Update to multiplayer-compatible system (enum? spawn?)
	private static Camera_FollowObject Instance;

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
			Instance.GetComponent<Cinemachine.CinemachineClearShot>().Follow = objectToFollow;
			Instance.GetComponent<Cinemachine.CinemachineClearShot>().LookAt = objectToFollow;
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
}
