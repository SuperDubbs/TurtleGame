using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleAnimator : MonoBehaviour
{
    Animator animator;
    Turtle turtle;
    public Transform graphicsTransform;
    

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        turtle = GetComponent<Turtle>();
    }

    // Update is called once per frame
    void Update()
    {
        float speedPercent = 1;
        if (turtle.movedirection.magnitude == 0f)
        {
            speedPercent = 0f;
        }
        else
        {
            speedPercent = 1f;
            Quaternion targetRotation = Quaternion.LookRotation(new Vector3(turtle.movedirection[0],0f,turtle.movedirection[2]), Vector3.up);
            graphicsTransform.rotation = Quaternion.Lerp(graphicsTransform.rotation, targetRotation, 0.2f);
            //graphicsTransform.Rotate(-90,0,0);
        }
        
        animator.SetFloat("speedPercent", speedPercent, 0.1f, Time.deltaTime);

    }
}
