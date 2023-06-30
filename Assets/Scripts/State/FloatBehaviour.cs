using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatBehaviour : StateMachineBehaviour
{
    public string floatName;
    public bool updateStateEnter;
    public bool updateStateExit;
    public bool updateStateMachineEnter;
    public bool updateStateMachineExit;
    public float valueEnter;
    public float valueExit;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (updateStateEnter)
        {
            animator.SetFloat(floatName, valueEnter);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (updateStateExit)
        {
            animator.SetFloat(floatName, valueExit);
        }
    }

    override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    {
        if (updateStateMachineEnter)
        {
            animator.SetFloat(floatName, valueEnter);
        }
    }

    // OnStateMachineExit is called when exiting a state machine via its Exit Node
    override public void OnStateMachineExit(Animator animator, int stateMachinePathHash)
    {
        if (updateStateMachineExit)
        {
            animator.SetFloat(floatName, valueExit);
        }
    }
}
