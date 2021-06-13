using UnityEngine;

public class SetupEraser : StateMachineBehaviour {
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        animator.GetComponent<Enemy>().Setup();
    }
}
