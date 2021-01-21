using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conversation : MonoBehaviour
{

    public enum ConversationType { Happy, Angry };
    public ConversationType conversationType;
    private Animator animator;
    private int animationNr = 0;

    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();

        StartCoroutine(conversationCooldown());
    }


    IEnumerator conversationCooldown()
    {
        float cooldown = Random.Range(1f, 5f);
        animateConversation();

        yield return new WaitForSeconds(cooldown);

        StartCoroutine(conversationCooldown());
    }


    void animateConversation()
    {
        float oldAnimationNr = animationNr;

        if (conversationType == ConversationType.Happy)
        {
            int amountOfHappyAnimations = 1;
            animationNr = GetNumber(1, amountOfHappyAnimations);
            while (animationNr == oldAnimationNr)
            {
                animationNr = GetNumber(1, amountOfHappyAnimations);
            }
            happyConversation();
        }
        else if (conversationType == ConversationType.Angry)
        {
            int amountOfAngryAnimations = 6;
            animationNr = GetNumber(1, amountOfAngryAnimations);
            while (animationNr == oldAnimationNr)
            {
                animationNr = GetNumber(1, amountOfAngryAnimations);
            }
            angryConverstation();
        }
    }

    private void angryConverstation()
    {
        switch (animationNr)
        {
            case 1:
                animator.SetTrigger("isCocky");
                break;
            case 2:
                animator.SetTrigger("isUninterested");
                break;
            case 3:
                animator.SetTrigger("isDissaproving");
                break;
            case 4:
                animator.SetTrigger("isAngry");
                break;
            case 5:
                animator.SetTrigger("isDismissing");
                break;
            case 6:
                animator.SetTrigger("isAgreeing");
                break;
            default:
                break;
        }
    }
    private void happyConversation()
    {
        switch (animationNr)
        {
            case 1:
                animator.SetTrigger("isHappy");
                break;

        }
    }


    private int GetNumber(int min, int max)
    {
        max++;
        return Random.Range(min, max);
    }
}
