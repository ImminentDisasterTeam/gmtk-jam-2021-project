using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SumFinisher : StateMachineBehaviour
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        animator.transform.parent.GetComponent<Player>().finishedSum = true;
    }
}
