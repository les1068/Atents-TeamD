using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerAnimSet : StateMachineBehaviour
{
    //OnStateMachineExit is called when exiting a state machine via its Exit Node
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!animator.IsInTransition(layerIndex))           //layerIndex번째 레이어가  트랜지션 중일때가 아니면
        {
             animator.SetInteger("ScenePlayerIndex", SceneIndex());
        }
    }

    int SceneIndex()
    {
        if (SceneManager.GetActiveScene().name == "TEST_ALL(Scrolling)" || SceneManager.GetActiveScene().name == "Test_joo_map")
        { return 1; }
        else
        {
            return -1;
        }
    }
}
