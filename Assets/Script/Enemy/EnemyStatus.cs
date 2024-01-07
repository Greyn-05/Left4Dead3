using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    [Header("Enemy Status")]
    [SerializeField]
    private float walkSpeed;
    [SerializeField]
    private float runSpeed;
    [SerializeField]
    private int attackDamage;

    public float WalkSpeed => walkSpeed;
    public float RunSpeed => runSpeed;
    public int AttackDamage => attackDamage;

}
