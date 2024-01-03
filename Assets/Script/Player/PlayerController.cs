using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float m_speed = 1.0f;

    [Header("Keys")]
    public KeyCode m_keyF = KeyCode.F;
    public KeyCode m_keySpace = KeyCode.Space;

    [HideInInspector]
    private CharacterController m_cc;

    private float m_movementY = 0.0f;//���������� ���� Y������ ���� 
    private Vector3 m_movement;//ĳ���� Transform ���� �����̷��� ����
    private Vector3 m_movePosition;//World Transform ���� �����̰� �Ϸ��� ����

    string InputH = "Horizontal";
    string InputV = "Vertical";

    private float m_gravity = -9.8f;

    bool m_jumped = false;

    private void Start()
    {
        m_cc = GetComponent<CharacterController>();
    }

    //ĳ������ �̵��Ÿ��� ����
    public float GetMagnitude()
    {
        return m_movement.magnitude;
    }

    //ĳ���Ͱ� �̵��ϰ� �ִ� ������ ����
    public float GetDirection()
    {
        return GetMagnitude() > 0 ? Mathf.Atan2(m_movePosition.x, m_movePosition.z) * Mathf.Rad2Deg : 0;
    }


    //��
    public bool IsJumped()
    {
        if (m_jumped)
        {
            m_jumped = false;
            return true;
        }

        return false;
    }

    //ĳ���Ͱ� ���� ����ִ��� Ȯ��
    public bool IsGrounded()
    {
        return m_cc.isGrounded;
    }

    //ĳ���͸� ȸ��
    public void Rotate(Quaternion angle)
    {
        transform.rotation = angle;
    }

    //ĳ������ ������(�յ��¿�)�� ������Ʈ��
    public void UpdateInput()
    {
        m_movement.x = Input.GetAxis(InputH) * m_speed;
        m_movement.z = Input.GetAxis(InputV) * m_speed;
        m_movementY += IsGrounded() ? 0 : (m_gravity*Time.deltaTime);

        m_jumped = m_cc.isGrounded ? Input.GetKeyDown(m_keySpace) : false;//����Ű�� ������ �� ����
        if (m_jumped)
        {
            m_movementY = 4.0f;
        }

        m_movePosition.Set(m_movement.x, m_movementY, m_movement.z);
        m_cc.Move(transform.rotation * m_movePosition * Time.deltaTime);
    }

}
