using UnityEngine;

public class AI_Search : CustomBehaviour
{
	[SerializeField]
	private float stoppingDistance = 5;

	public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
	{
		base.OnStateEnter(animator, animatorStateInfo, layerIndex);

		this.navigator.NavAgent.stoppingDistance = this.stoppingDistance;
	}

	public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
	{
		// Navigate towards target's last known position
		this.navigator.NavAgent.SetDestination(this.navigator.LastPOI);

		// TODO: Add timeout -> exit
	}
}
