using UnityEngine;

public enum PlayerState//ĳ���� ����
{
    Dead,//���
    Heal,//ġ��
    Reload,//����
    Damage,//���� ����
    Idle,//�ƹ� �ൿ�� ����
}

public class PlayerManager : MonoBehaviour
{

    [HideInInspector]
    private PlayerController m_playerController;
    private PlayerAnimationManager m_animationManager;
    private ThirdPersonCamera m_tpCamera;

    private float m_healthPoint;

    private PlayerState m_state = PlayerState.Idle;

    private void Start()
    {
        m_healthPoint = 100.0f;

        m_playerController = GetComponent<PlayerController>();
        m_tpCamera = GetComponent<ThirdPersonCamera>();
        m_animationManager = transform.GetChild(0).GetComponent<PlayerAnimationManager>();

        m_tpCamera.Initialize();
    }

    private void Update()
    {
        if(m_healthPoint<=0)//ĳ������ ü���� ������
        {
            m_state = PlayerState.Dead;//ĳ������ ���� = ����
            m_tpCamera.ToggleCameraLock();//ī�޶� ȸ��, ������ ����
        }else
        {
            m_tpCamera.UpdateCamera();
            m_playerController.Rotate(m_tpCamera.GetDirection());
            m_playerController.UpdateInput();
            m_animationManager.UpdateAnimation(m_state, m_playerController.GetDirection(), m_playerController.GetMagnitude(), m_playerController.IsJumped(), m_playerController.IsGrounded());//ĳ������ ���� ���¿� ���� �ִϸ��̼� ��ȯ
        }
    }
}
