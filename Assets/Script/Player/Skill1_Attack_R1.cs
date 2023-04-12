using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill1_Attack_R1 : StateMachineBehaviour
{
    Skill1 skill1;

    private void Awake()
    {
        skill1 = FindObjectOfType<Skill1>();
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        skill1.SkillComboDown();
    }
}
