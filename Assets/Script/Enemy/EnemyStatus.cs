using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    [Header("Walk, Run")]
    [SerializeField]
    private float walkSpeed;
    [SerializeField]
    private float runSpeed;
    [SerializeField]
    private float attackDamage;
    [SerializeField]
    private float defence;

    public float WalkSpeed => walkSpeed;
    public float RunSpeed => runSpeed;
    public float AttackDamage => attackDamage;
    public float Defence => defence;
}
