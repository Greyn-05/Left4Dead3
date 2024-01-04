using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    private bool m_isGround;
    private bool m_isSprint;
    private Animator m_animator;

    private int InputAngle = Animator.StringToHash("InputAngle");
    private int InputMagnitude = Animator.StringToHash("InputMagnitude");
    private int IsHeal = Animator.StringToHash("IsHeal");
    private int IsDead = Animator.StringToHash("IsDead");
    private int IsJumped = Animator.StringToHash("IsJumped");
    private int IsGrounded = Animator.StringToHash("IsGrounded");
    private int IsSprint = Animator.StringToHash("IsSprint");
    private int GroundDistance = Animator.StringToHash("GroundDistance");

    private void Start()
    {
        m_animator = GetComponent<Animator>();
    }

    //캐릭터 상태에 따라 애니메이션 전환하는 함수
    public void UpdateAnimation(PlayerState state, float angle, float magnitude, bool isjumped, bool isground, float distance)
    {
        m_animator.SetFloat(InputAngle, angle);
        m_animator.SetFloat(InputMagnitude, magnitude);
        m_animator.SetFloat(GroundDistance, distance);
        if (isjumped) m_animator.SetTrigger(IsJumped);//점프 키를 눌렀을 때 트리거 발동
        m_animator.SetBool(IsGrounded, isground);

        switch(state)
        {
            case PlayerState.Dead:
                m_animator.SetTrigger(IsDead);
                break;
            case PlayerState.Heal:
                m_animator.SetTrigger(IsHeal);
                break;
        }
    }
}
