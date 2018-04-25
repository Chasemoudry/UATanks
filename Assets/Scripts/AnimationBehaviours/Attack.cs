using UnityEngine;

namespace AnimationBehaviours
{
	/// <summary>
	/// Custom StateMachineBehaviour which handles attack events.
	/// </summary>
	public class Attack : StateMachineBehaviour
	{
		public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
		{
			// Resets animator's attack boolean to prevent queued inputs
			animator.SetBool("Attack", false);
		}
	}
}