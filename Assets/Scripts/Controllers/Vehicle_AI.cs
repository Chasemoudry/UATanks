using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle_AI : MonoBehaviour, INavigator
{
    public Transform[] PatrolWaypoints { get { return this.patrolWaypoints; } }

    public float HearingDistance { get { return this.hearingDistance; } }

    [SerializeField]
    private Transform[] patrolWaypoints;

    [Header("Hearing")]
    [SerializeField]
    private float hearingDistance;

    [Header("Sight")]
    [SerializeField]
    private LayerMask sightLayerMask;
    [SerializeField, Range(0, 360)]
    private int sightFOV_Total;
    [SerializeField, Range(0, 360)]
    private int sightFOV_Focused;

    private void Start()
    {

    }
}
