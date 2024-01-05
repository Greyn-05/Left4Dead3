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

    private void Start()
    {
        m_animator = GetComponent<Animator>();
    }

    public void DoJump()
    {
        m_animator.SetTrigger(IsJumped);
    }

    //캐릭터 상태에 따라 애니메이션 전환하는 함수
    public void UpdateAnimation(PlayerState state, float angle, float magnitude, bool isground)
    {
        m_animator.SetFloat(InputAngle, angle);
        m_animator.SetFloat(InputMagnitude, magnitude);
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
