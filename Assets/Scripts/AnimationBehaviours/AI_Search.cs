using UnityEngine;

public class AI_Search : CustomBehaviour
{
	[SerializeField, Tooltip("Does the AI approach the target's last known position?")]
	private bool canApproach = true;
	[SerializeField, Range(0, 5), Tooltip("")]
	private float movementSpeed = 1;

	private Vector3 target_LastKnownPosition;

	public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
	{
		this.target_LastKnownPosition = this.navigator.LastPOI;
	}

	public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
	{
		if (this.canApproach)
		{
			// TODO: Navigate towards target's last known position
		}
	}
}
