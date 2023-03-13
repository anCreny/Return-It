using UnityEngine;

public class CoreSpawnListener : StateMachineBehaviour
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.GetComponent<Core>().HasSpawned();
    }
}
