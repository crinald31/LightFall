using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeRemove : StateMachineBehaviour
{
    public float fadeTime = 0.5f;
    private float timeElapsed = 0f;
    SpriteRenderer sr;
    GameObject objToRemove;
    Color startColor;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timeElapsed = 0f;
        sr = animator.GetComponent<SpriteRenderer>();
        objToRemove = animator.gameObject;
        startColor = sr.color;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float newAlpha = startColor.a * (1 - (timeElapsed / fadeTime));

        timeElapsed += Time.deltaTime;
        if (timeElapsed > fadeTime)
        {
            Destroy(objToRemove);
        }
        sr.color = new Color(startColor.r, startColor.g, startColor.b, newAlpha);
    }
}
