using UnityEngine;

public class CustomBehaviour : StateMachineBehaviour
{
	protected IVehicle controller;
	protected INavigator navigator;

	public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
	{
		this.controller = animator.GetComponent<IVehicle>();
		this.navigator = animator.GetComponent<INavigator>();
	}
}
