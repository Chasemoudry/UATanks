using UnityEngine;

namespace CustomBehaviours
{
	/// <summary>
	/// Custom StateMachineBehaviour which navigates animator object towards ISensor's CurrentTarget.
	/// </summary>
	public class AI_Follow_Target : StateMachineBehaviour
	{
		/// <summary>Stopping distance of the NavMeshAgent for this behaviour.</summary>
		[SerializeField, Tooltip("Stopping distance of the NavMeshAgent for this behaviour.")]
		private float stoppingDistance = 10;

		/// <summary>Animator object's ISensor component.</summary>
		private ISensor Sensor;
		/// <summary>Animator object's INavigator component.</summary>
		private INavigator Navigator;

		public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
		{
			this.Sensor = animator.GetComponent<ISensor>();
			this.Navigator = animator.GetComponent<INavigator>();

			// Set stopping distance for this behaviour
			this.Navigator.NavAgent.stoppingDistance = this.stoppingDistance;
		}

		public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
		{
			// Navigate towards current target
			this.Navigator.NavAgent.SetDestination(this.Sensor.CurrentTarget.position);
		}
	}
}