using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    public enum CharacterState//애니메이션 상태
    {
        StrafeLeft,//왼쪽 이동
        StrafeRight,//오른쪽 이동
        Forward,//전진
        Backward,//후진
        Die,//사망
        Heal,//치료
        Aim,//조준
        Jump,//점프
        Idle,//대기
    }

    private bool m_isGround;
    private bool m_isSprint;
    private Animator m_animator;

    private int Forward = Animator.StringToHash("Forward");
    private int Backward = Animator.StringToHash("Backward");
    private int StrafeLeft = Animator.StringToHash("StrafeLeft");
    private int StrafeRight = Animator.StringToHash("StrafeRight");
    private int IsAimed = Animator.StringToHash("IsAimed");
    private int IsDead = Animator.StringToHash("IsDead");
    private int IsJumped = Animator.StringToHash("IsJumped");
    private int IsGrounded = Animator.StringToHash("IsGrounded");
    private int IsSprint = Animator.StringToHash("IsSprint");
    private int GroundDistance = Animator.StringToHash("GroundDistance");


    private CharacterState st = CharacterState.Idle;//테스트용

    private void Start()
    {
        m_animator = GetComponent<Animator>();
    }

    private void Update()
    {
        UpdateAnimation(st);
    }

    //캐릭터 상태에 따라 애니메이션 전환하는 함수
    public void UpdateAnimation(CharacterState state)
    {
        switch (state)
        {
            case CharacterState.StrafeLeft:
                m_animator.SetTrigger(StrafeLeft);
                break;
            case CharacterState.StrafeRight:
                m_animator.SetTrigger(StrafeRight);
                break;
            case CharacterState.Forward:
                m_animator.SetTrigger(Forward);
                break;
            case CharacterState.Backward:
                m_animator.SetTrigger(Backward);
                break;
            case CharacterState.Jump:
                m_animator.SetTrigger(IsJumped);
                break;
            case CharacterState.Die:
                m_animator.SetTrigger(IsDead);
                break;
            case CharacterState.Aim:
                m_animator.SetTrigger(IsAimed);
                break;
        }
    }
}
