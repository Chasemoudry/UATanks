using UnityEngine;

[DisallowMultipleComponent, RequireComponent(typeof(Cinemachine.CinemachineClearShot))]
public class Camera_FollowObject : MonoBehaviour
{
	private static Camera_FollowObject Instance;

	public static Transform ObjectToFollow
	{
		get
		{
			return objectToFollow;
		}
		set
		{
			objectToFollow = value;
			Instance.GetComponent<Cinemachine.CinemachineClearShot>().Follow = objectToFollow;
			Instance.GetComponent<Cinemachine.CinemachineClearShot>().LookAt = objectToFollow;
		}
	}

	private static Transform objectToFollow;

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
}
