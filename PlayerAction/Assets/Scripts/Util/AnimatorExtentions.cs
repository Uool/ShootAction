using System;
using UnityEngine;

public static class AnimatorExtentions
{
    public static void SetAnimatorTrigger(this Animator animator, AnimatorTrigger trigger)
    {
        Debug.Log($"SetAnimatorTrigger: {trigger} - {(int)trigger}");
        animator.SetInteger(AnimationParameters.TriggerNumber, (int)trigger);
        animator.SetTrigger(AnimationParameters.Trigger);
    }

    public static void SetActionTrigger(this Animator animator, AnimatorTrigger trigger, int actionNumber)
    {
        Debug.Log($"SetActionTrigger: {trigger} - {(int)trigger} - action {actionNumber}");
        animator.SetInteger(AnimationParameters.Action, actionNumber);
        SetAnimatorTrigger(animator, trigger);
    }
}
