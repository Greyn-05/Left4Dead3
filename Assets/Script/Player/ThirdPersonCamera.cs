using System.Collections;
using System.Collections.Generic;
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
    private Vector2 m_axis;

    bool m_lock = true;//카메라 회전 잠금

    string InputX = "Mouse X";
    string InputY = "Mouse Y";

    private void Start()
    {
        m_camera = Camera.main.transform.parent.gameObject;
        ToggleCameraLock();
    }

    public void ToggleCameraLock()
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

    public void UpdateCamera()
    {
        if (!m_lock)
        {
            RotateCamera();
            MoveCamera();
        }
    }

    public float GetDirection()
    {
        return m_camera.transform.eulerAngles.y;
    }

    private void MoveCamera()
    {
        if (m_camera != null)
            m_camera.transform.position = transform.position;
    }

    private void RotateCamera()
    {
        m_axis.x += Input.GetAxis(InputX) * m_sensitivityX;
        m_axis.y -= Input.GetAxis(InputY) * m_sensitivityY;

        while (m_axis.x < -360 || m_axis.x > 360)//마우스가 화면 밖으로 나가지 않게
        {
            if (m_axis.x < -360)
                m_axis.x += 360;
            if (m_axis.x > 360)
                m_axis.x -= 360;
        }

        if (m_axis.magnitude != 0)//마우스 움직임이 있을때만
        {
            m_axis.x = Mathf.Clamp(m_axis.x, m_minX, m_maxX);
            m_axis.y = Mathf.Clamp(m_axis.y, m_minY, m_maxY);

            Quaternion newRot = Quaternion.Euler(m_axis.y, m_axis.x, 0);
            m_camera.transform.rotation = Quaternion.Slerp(m_camera.transform.rotation, newRot, 16 * Time.deltaTime);
        }
    }
}
