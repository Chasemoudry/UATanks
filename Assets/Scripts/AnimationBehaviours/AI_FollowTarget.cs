using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_FollowTarget : CustomBehaviour
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
		if (this.navigator.CurrentTarget != null)
		{
			this.navigator.NavAgent.SetDestination(this.navigator.CurrentTarget.position);
		}
	}
}
