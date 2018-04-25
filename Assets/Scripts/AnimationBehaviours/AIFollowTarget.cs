using UnityEngine;

namespace AnimationBehaviours
{
	/// <inheritdoc />
	/// <summary>
	/// Custom StateMachineBehaviour which navigates animator object towards ISensor's CurrentTarget.
	/// </summary>
	public class AIFollowTarget : StateMachineBehaviour
	{
		/// <summary>Stopping distance of the NavMeshAgent for this behaviour.</summary>
		[SerializeField, Tooltip("Stopping distance of the NavMeshAgent for this behaviour.")]
		private float _stoppingDistance = 5f;

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
			if (this._sensor.CurrentTarget != null)
			{
				// Navigate towards current target
				this._navigator.NavAgent.SetDestination(this._sensor.CurrentTarget.position);
			}

			if (this._navigator.NavAgent.velocity.magnitude <= 1)
			{
				animator.transform.LookAt(new Vector3(
					this._sensor.LastPOI.x,
					animator.transform.position.y,
					this._sensor.LastPOI.z
				));
			}
		}
	}
}