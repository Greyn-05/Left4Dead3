using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyBehaviorState
{
    none = -1,
    idle = 0,       //기본 상태
    wander = 1,     //배회
    suspicion = 2,  //의심
    pursuit = 3,    //추적
    attack = 4,      //공격
    death = 5
}
public class EnemyBehavior : MonoBehaviour
{
    [Header("Pursuit")]
    [SerializeField]
    private float suspicionRange = 40;

    [SerializeField]
    private float StartPursuitRange= 20;

    [SerializeField]
    private float QuitpursuitRange = 30;

    [SerializeField]
    private float AttackRange = 0.5f;


    private EnemyBehaviorState enemyState = EnemyBehaviorState.none;

    private EnemyStatus status;
    private NavMeshAgent navMeshAgent;
    public Transform TargetPlayer;
    private EnemyAnimationController animationController;
    private AudioSource Audio;
    private BoxCollider boxCollider;
    private CharacterController characterController;
    public List<AudioClip> Walksound = new List<AudioClip> { };
    public List<AudioClip> Attacksound = new List<AudioClip> { };
    public List<AudioClip> Hitsound = new List<AudioClip> { };

    Coroutine TestCoroutine = null;

    private int enemyHP = 100;
    private void Awake()
    {
        status = GetComponent<EnemyStatus>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        animationController = GetComponent<EnemyAnimationController>();
        Audio = GetComponent<AudioSource>();
        boxCollider = GetComponent<BoxCollider>();
        characterController = GetComponent<CharacterController>();

        navMeshAgent.updateRotation = false;
    }

    public void SetTargetPlayer(GameObject Player)
    {
        TargetPlayer = Player.transform;
    }

    private void OnEnable()
    {
        ChangeState(EnemyBehaviorState.idle);
    }

    private void OnDisable()
    {
        StopCoroutine(enemyState.ToString());

        enemyState = EnemyBehaviorState.none;
    }

    public void ChangeState(EnemyBehaviorState newState)
    {
        if ((enemyState == newState)&&(enemyState!=EnemyBehaviorState.attack))
        {
            return;
        }

        /*if(TestCoroutine != null)
        {
            StopCoroutine(TestCoroutine);
        }*/
        StopCoroutine(enemyState.ToString());
        enemyState = newState;
        StartCoroutine(enemyState.ToString());
    }

    private IEnumerator idle()
    {
        //Debug.Log("Start Idle");
        enemyState = EnemyBehaviorState.idle;
        //animationController.MotionState = 0;
        StartCoroutine("AutoChangeFromIdleToWander");
        
        while ( true )
        {
            CalculateDistanceToSelectState();

            yield return null;
        }
    }

    private IEnumerator AutoChangeFromIdleToWander()
    {
        float changeTime = Random.Range(3, 5);

        yield return new WaitForSeconds(changeTime);

        ChangeState(EnemyBehaviorState.wander);
    }

    private IEnumerator wander()
    {
        //Debug.Log("Start Wander");
        enemyState = EnemyBehaviorState.wander;
        animationController.RunSpeed = 1f;
        animationController.SetAnimation(false);
        float currentTime = 0;
        float MaxTime = 10;

        navMeshAgent.speed = status.WalkSpeed;

        navMeshAgent.SetDestination(CalculateWanderPosition());


        Vector3 to = new Vector3(navMeshAgent.destination.x, 0, navMeshAgent.destination.z);
        Vector3 from = new Vector3(transform.position.x, 0, transform.position.z);
        transform.rotation = Quaternion.LookRotation(to - from);

        while(true)
        {
            currentTime += Time.deltaTime;
            to = new Vector3(navMeshAgent.destination.x, 0, navMeshAgent.destination.z);
            from = new Vector3(transform.position.x, 0, transform.position.z);

            

            if (((to-from).sqrMagnitude < 1f)||(currentTime >= MaxTime))
            {
                ChangeState(EnemyBehaviorState.idle);
            }

            //Debug.Log((to - from).sqrMagnitude);
            CalculateDistanceToSelectState();

            yield return null;

        }
    }

    private Vector3 CalculateWanderPosition()
    {
        float wanderRadius = 10;
        int wanderdir = 0;
        int wanderdirMin = 0;
        int wanderdirMax = 360;

        Vector3 rangePosition = transform.position;
        Vector3 rangeScale = Vector3.one * 100.0f;

        wanderdir = Random.Range(wanderdirMin, wanderdirMax);
        Vector3 targetPosition = transform.position + SetAngle(wanderRadius, wanderdir);
        
        targetPosition.x = Mathf.Clamp(targetPosition.x, rangePosition.x - rangeScale.x * 0.5f, rangePosition.x + rangeScale.x * 0.5f);
        targetPosition.y = transform.position.y;
        targetPosition.z = Mathf.Clamp(targetPosition.z, rangePosition.z - rangeScale.z * 0.5f, rangePosition.z + rangeScale.z * 0.5f);
        return targetPosition;
    }

    private Vector3 SetAngle(float radius, int angle)
    {
        Vector3 position = Vector3.zero;

        position.x = Mathf.Cos(angle) * radius;
        position.z = Mathf.Sin(angle) * radius;

        return position;
    }

    private IEnumerator pursuit()
    {
        //Debug.Log("Start pursuit");
        enemyState = EnemyBehaviorState.pursuit;
        

        while (true)
        {
            animationController.RunSpeed = 2f;
            animationController.SetAnimation(false);
            navMeshAgent.speed = status.RunSpeed;

            navMeshAgent.SetDestination(TargetPlayer.position);
            //Debug.Log(navMeshAgent.destination);

            LookRotationToTarget();

            CalculateDistanceToSelectState();

            yield return null;
        }

        
    }

    private void LookRotationToTarget()
    {
        Vector3 dest = new Vector3(TargetPlayer.position.x, 0, TargetPlayer.position.z);
        Vector3 init = new Vector3(transform.position.x, 0, transform.position.z);

        transform.rotation = Quaternion.LookRotation(dest - init);
    }

    private void CalculateDistanceToSelectState()
    {
        if(TargetPlayer==null)
        {
            return;
        }

        float distance = Vector3.Distance(TargetPlayer.position, transform.position);
        //Debug.Log($"distance : {distance}");
        if(distance <= AttackRange)
        {
            ChangeState(EnemyBehaviorState.attack);
        }
        else if(distance <= StartPursuitRange)
        {
            ChangeState(EnemyBehaviorState.pursuit);
        }
        else if(distance >= QuitpursuitRange)
        {
            //ChangeState(EnemyBehaviorState.wander);
        }
    }

    private IEnumerator attack()
    {
        //Debug.Log("Start attack");
        
        enemyState = EnemyBehaviorState.attack;
        animationController.Attack = (float)Random.Range(1, 5);

        while (true)
        {
            PlaySound(Attacksound[Random.Range(0, 4)]);
            animationController.SetAnimation(true);
            navMeshAgent.SetDestination(transform.position);

            LookRotationToTarget();
            StartCoroutine(StartAttack());
            yield return new WaitForSeconds(4f);

            CalculateDistanceToSelectState();
        }
        
    }

    private IEnumerator StartAttack()
    {
        yield return new WaitForSeconds(2f);
        float distance = Vector3.Distance(TargetPlayer.position, transform.position);
        if (distance <= AttackRange)
        {
            //플레이어 데미지 주기
            Debug.Log("Player Hit!!!");
        }

    }

    private void PlaySound(AudioClip clip)
    {
        Audio.Stop();
        Audio.clip = clip;
        Audio.Play();

    }

    private void RageMode()
    {
        StartPursuitRange = 180;
        QuitpursuitRange = 200;
    }

    public void getHit(int Damage)
    {
        enemyHP -= Damage;

        if (enemyHP <= 0)
        {
            ChangeState(EnemyBehaviorState.death);
        }
    }

    private IEnumerator death()
    {
        animationController.Play("Zombie Death", -1, 0);
        boxCollider.enabled = false;
        navMeshAgent.enabled = false;
        characterController.enabled = false;
        yield return new WaitForSeconds(3f);
    }

    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.blue;
        //Gizmos.DrawRay(transform.position, navMeshAgent.destination - transform.position);

        /*Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, StartPursuitRange);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, QuitpusuitRange);

        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, AttackRange);*/

    }
    
}
