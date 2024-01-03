using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{

    [Header("MouseSensitivity")]
    public float m_sensitivityX = 5f;
    public float m_sensitivityY = 5f;

    [Header("Camera")]
    public GameObject m_camera;

    [Header("MaxAngle")]
    public float m_maxX = 360.0f;
    public float m_maxY = 50.0f;

    [Header("MinAngle")]
    public float m_minX = -360.0f;
    public float m_minY = 0.0f;


    [HideInInspector]
    private Vector2 m_axis = Vector2.zero;

    bool m_lock = true;//ī�޶� ȸ�� ���

    string InputX = "Mouse X";
    string InputY = "Mouse Y";

    public void Initialize()
    {
        if (m_camera == null) m_camera = Camera.main.transform.parent.gameObject;
        m_camera.transform.rotation = Quaternion.identity;

        ToggleCameraLock();
    }

    public void ToggleCameraLock()//ī�޶� ��� �¿���
    {
        m_lock = !m_lock;

        if (m_lock)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void UpdateCamera()//ī�޶� ������Ʈ
    {
        if (!m_lock)
        {
            RotateCamera();
            MoveCamera();
        }
    }

    public Quaternion GetDirection()//ī�޶� ���� ����, y����
    {
        return Quaternion.Euler(0, m_camera.transform.rotation.eulerAngles.y, 0);
    }

    private void MoveCamera()//ī�޶� ��ġ ����
    {
        if (m_camera != null)
            m_camera.transform.position = transform.position;
    }

    private void RotateCamera()//ī�޶� ȸ��
    {
        m_axis.x += Input.GetAxis(InputX) * m_sensitivityX;
        m_axis.y -= Input.GetAxis(InputY) * m_sensitivityY;

        while (m_axis.x < -360 || m_axis.x > 360)//���콺�� ȭ�� ������ ������ �ʰ�
        {
            if (m_axis.x < -360)
                m_axis.x += 360;
            if (m_axis.x > 360)
                m_axis.x -= 360;
        }

        if (m_axis.magnitude != 0)//���콺 �������� ��������
        {
            m_axis.x = Mathf.Clamp(m_axis.x, m_minX, m_maxX);//�ִ� �ּ� ���� ����
            m_axis.y = Mathf.Clamp(m_axis.y, m_minY, m_maxY);

            Quaternion newRot = Quaternion.Euler(m_axis.y, m_axis.x, 0);
            m_camera.transform.rotation = Quaternion.Slerp(m_camera.transform.rotation, newRot, 16 * Time.deltaTime);
        }
    }
}
