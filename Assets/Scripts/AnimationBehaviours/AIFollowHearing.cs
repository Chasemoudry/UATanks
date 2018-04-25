using UnityEngine;

namespace AnimationBehaviours
{
	/// <summary>
	/// Custom StateMachineBehaviour which navigates animator object towards ISensor's LastPOI.
	/// </summary>
	public class AIFollowHearing : StateMachineBehaviour
	{
		/// <summary>Stopping distance of the NavMeshAgent for this behaviour.</summary>
		[SerializeField, Tooltip("Stopping distance of the NavMeshAgent for this behaviour.")]
		private float _stoppingDistance = 10;

		/// <summary>Animator object's ISensor component.</summary>
		private ISensor _sensor;
		/// <summary>Animator object's INavigator component.</summary>
		private INavigator _navigator;

		public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
		{
			this._sensor = animator.GetComponent<ISensor>();
			this._navigator = animator.GetComponent<INavigator>();

			// Set stopping distance for this behaviour
			this._navigator.NavAgent.stoppingDistance = this._stoppingDistance;
		}

		public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
		{
			// Navigate towards target's last known position
			this._navigator.NavAgent.SetDestination(this._sensor.LastPOI);

			if (this._navigator.NavAgent.velocity == Vector3.zero)
			{
				animator.transform.LookAt(new Vector3(
					this._sensor.LastPOI.x,
					animator.transform.position.y,
					this._sensor.LastPOI.z
				));
			}
			// TODO: Add timeout -> exit once destination is reached
		}
	}
}