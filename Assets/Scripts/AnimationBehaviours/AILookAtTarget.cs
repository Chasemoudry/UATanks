using UnityEngine;

namespace AnimationBehaviours
{
	/// <inheritdoc />
	/// <summary>
	/// Custom StateMachineBehaviour which rotates animator object towards ISensor's CurrentTarget.
	/// </summary>
	public class AILookAtTarget : StateMachineBehaviour
	{
		/// <summary>Animator object's ISensor component.</summary>
		private ISensor _sensor;
		/// <summary>Animator object's INavigator component.</summary>
		private INavigator _navigator;

		public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
		{
			this._sensor = animator.GetComponent<ISensor>();
			this._navigator = animator.GetComponent<INavigator>();

			if (this._navigator == null)
			{
				return;
			}

			// Stop navigation
			this._navigator.NavAgent.speed = 0;
			this._navigator.NavAgent.isStopped = true;
		}

		public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
		{
			// Look at the current target
			// TODO: Change to Quaternion.LookRotation for rotation slerping
			animator.transform.LookAt(
				new Vector3(
					this._sensor.LastPOI.x,
					animator.transform.position.y,
					this._sensor.LastPOI.z
				));
		}

		public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
		{
			// if: animator object has an INavigator, resume navigation
			if (this._navigator != null)
			{
				this._navigator.NavAgent.isStopped = false;
			}
		}
	}
}