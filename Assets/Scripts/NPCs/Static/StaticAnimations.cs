using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticAnimations : MonoBehaviour
{
    public enum AnimationType { Leaning, LeaningForward, SittingFootTapping, SittingIdle, Sitting, Idle, Driving };
    public AnimationType animationType;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        if (animationType == AnimationType.Leaning)
        {
            animator.SetBool("isLeaning", true);
        }
        else if (animationType == AnimationType.LeaningForward)
        {
            animator.SetBool("isLeaningForward", true);
        }
        else if (animationType == AnimationType.SittingFootTapping)
        {
            animator.SetBool("isSittingFootTapping", true);
        }
        else if (animationType == AnimationType.SittingIdle)
        {
            animator.SetBool("isSittingIdle", true);
        }
        else if (animationType == AnimationType.Sitting)
        {
            animator.SetBool("isSitting", true);
        }
        else if (animationType == AnimationType.Idle)
        {
            animator.SetBool("isIdle", true);
        }
        else if (animationType == AnimationType.Driving)
        {
            animator.SetBool("isDriving", true);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
