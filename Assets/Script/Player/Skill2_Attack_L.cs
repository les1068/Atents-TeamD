using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill2_Attack_L : StateMachineBehaviour
{
    Skill2 skill2;

    private void Awake()
    {
        skill2 = FindObjectOfType<Skill2>();
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        skill2.SkillComboDown();
    }
}
