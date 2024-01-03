using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    public enum CharacterState//�ִϸ��̼� ����
    {
        StrafeLeft,//���� �̵�
        StrafeRight,//������ �̵�
        Forward,//����
        Backward,//����
        Die,//���
        Heal,//ġ��
        Aim,//����
        Jump,//����
        Idle,//���
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


    private CharacterState st = CharacterState.Idle;//�׽�Ʈ��

    private void Start()
    {
        m_animator = GetComponent<Animator>();
    }

    private void Update()
    {
        UpdateAnimation(st);
    }

    //ĳ���� ���¿� ���� �ִϸ��̼� ��ȯ�ϴ� �Լ�
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
