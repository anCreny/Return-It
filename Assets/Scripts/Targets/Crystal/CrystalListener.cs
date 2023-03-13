using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalListener : StateMachineBehaviour
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.GetComponent<RhombusTarget>().HasSpawned();
    }
}
