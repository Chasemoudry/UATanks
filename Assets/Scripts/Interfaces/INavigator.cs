using UnityEngine;

public interface INavigator
{
    Transform[] PatrolWaypoints { get; }
    float HearingDistance { get; }
    LayerMask SightLayerMask { get; }
    int SightFOV_Total { get; }
    int SightFOV_Focused { get; }
}
