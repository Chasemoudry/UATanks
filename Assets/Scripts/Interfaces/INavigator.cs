using UnityEngine;

public interface INavigator
{
	Transform[] PatrolWaypoints { get; }
	Transform CurrentTarget { get; set; }
}
