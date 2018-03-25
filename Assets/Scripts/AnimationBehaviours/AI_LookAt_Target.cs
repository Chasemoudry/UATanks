using UnityEngine;

namespace CustomBehaviours
{
	/// <summary>
	/// Custom StateMachineBehaviour which rotates animator object towards ISensor's CurrentTarget.
	/// </summary>
	public class AI_LookAt_Target : StateMachineBehaviour
	{
		/// <summary>Animator object's ISensor component.</summary>
		private ISensor Sensor;
		/// <summary>Animator object's INavigator component.</summary>
		private INavigator Navigator;

		public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
		{
			this.Sensor = animator.GetComponent<ISensor>();
			this.Navigator = animator.GetComponent<INavigator>();

			// if: animator object has an INavigator, stop navigation
			if (this.Navigator != null)
			{
				this.Navigator.NavAgent.speed = 0;
				this.Navigator.NavAgent.isStopped = true;
			}
		}

		public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
		{
			// Look at the current target
			// TODO: Change to Quaternion.LookRotation for rotation slerping
			animator.transform.LookAt(
				new Vector3(
					this.Sensor.LastPOI.x,
					animator.transform.position.y,
					this.Sensor.LastPOI.z
				));
		}

		public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
		{
			// if: animator object has an INavigator, resume navigation
			if (this.Navigator != null)
			{
				this.Navigator.NavAgent.isStopped = false;
			}
		}
	}
}