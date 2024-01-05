using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator animator;
    // Start is called before the first frame update
    private void Awake()
    {
        //animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    public float MotionState
    {
        set => animator.SetFloat("MotionState", value);
        get => animator.GetFloat("MotionState");
    }

    public float Attack
    {
        set => animator.SetFloat("Attack", value);
        get => animator.GetFloat("Attack");
    }

    public float RunSpeed
    {
        set => animator.SetFloat("RunSpeed", value);
        get => animator.GetFloat("RunSpeed");
    }

    public void SetAnimation(bool onAttack) //°ø°Ý½Ã true
    {
        animator.SetBool("isAttack", onAttack);
        animator.SetBool("isMove", !onAttack);
    }


    public void Play(string stateName, int layer, float normalixedTime)
    {
        animator.Play(stateName, layer, normalixedTime);
    }
}
