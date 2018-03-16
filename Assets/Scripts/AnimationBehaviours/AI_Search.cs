using UnityEngine;

public class AI_Search : CustomBehaviour
{
	[SerializeField, Tooltip("Does the AI approach the target's last known position?")]
	private bool canApproach = true;

	private Vector3 target_LastKnownPosition;

	public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
	{
		this.target_LastKnownPosition = this.navigator.CurrentTarget.position;
	}

	public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
	{
		if (this.canApproach)
		{
			// Navigate towards target's last known position
		}
	}
}
