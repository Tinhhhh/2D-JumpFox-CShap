using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public static PlayerAnimation instance;
    private readonly int moveX = Animator.StringToHash("MoveX");
    private readonly int moveY = Animator.StringToHash("MoveY");
    private readonly int moving = Animator.StringToHash("Moving");
    private readonly int dead = Animator.StringToHash("Dead");
    private readonly int HoldJump = Animator.StringToHash("HoldJump");

    private Animator animator;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        animator = GetComponent<Animator>();
    }

    public void ShowDeadAnimation()
    {
        animator.SetTrigger(dead);
    }

    public void SetIdleAnimation(bool value)
    {
        animator.SetBool(moving, value);
    }

    public void SetMovingAnimation(float dirX)
    {
        animator.SetFloat(moveX, dirX);
    }

    public void SetJumpingAnimation(float dirY)
    {
        animator.SetFloat(moveY, dirY);
    }

    public void setHoldJumpAnimation(bool value)
    {
        animator.SetBool(HoldJump, value);
    }
}
