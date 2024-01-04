using UnityEngine;

public enum PlayerState//캐릭터 상태
{
    Dead,//사망
    Heal,//치료
    Reload,//장전
    Damage,//피해 입음
    Idle,//아무 행동도 안함
}

public class PlayerManager : MonoBehaviour
{

    [HideInInspector]
    private PlayerControl m_playerController;
    private PlayerAnimationManager m_animationManager;
    private ThirdPersonCamera m_tpCamera;

    private float m_healthPoint;
    private float m_maxHp;

    private PlayerState m_state = PlayerState.Idle;

    private void Start()
    {
        m_healthPoint = 100.0f;

        m_playerController = GetComponent<PlayerControl>();
        m_tpCamera = GetComponent<ThirdPersonCamera>();
        m_animationManager = transform.GetChild(0).GetComponent<PlayerAnimationManager>();

        m_tpCamera.Initialize();
    }

    private void Update()
    {
        if(m_healthPoint<=0)//캐릭터의 체력이 없을때
        {
            m_state = PlayerState.Dead;//캐릭터의 상태 = 죽음
            m_tpCamera.ToggleCameraLock();//카메라 회전, 움직임 고정
        }else
        {
            m_tpCamera.UpdateCamera();
            m_playerController.Rotate(m_tpCamera.GetDirection());
            m_playerController.UpdateInput();
            m_animationManager.UpdateAnimation(m_state, m_playerController.GetDirection(), m_playerController.GetMagnitude(), m_playerController.IsJumped(), m_playerController.IsGrounded(), m_playerController.GetGroundDistance());//캐릭터의 현재 상태에 따라 애니메이션 전환
        }
    }

    //플레이어의 상태를 변경하는 함수
    public void SetState(PlayerState state)
    {
        m_state = state;
    }

    //체력을 추가하거나 깍는 함수
    public void AddHealthPoint(float p)
    {
        m_healthPoint += p;
        m_healthPoint = m_healthPoint > m_maxHp ? m_maxHp : m_healthPoint;//최대 체력을 초과하면 최대치로 고정
    }

}
