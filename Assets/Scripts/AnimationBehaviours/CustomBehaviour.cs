using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomBehaviour : StateMachineBehaviour
{
    protected IVehicle controller;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        this.controller = animator.GetComponent<IVehicle>();
    }
}
