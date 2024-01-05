using UnityEngine;

public class PlayerCameraManager : MonoBehaviour
{

    [Header("MouseSensitivity")]
    public float m_sensitivityX = 5f;
    public float m_sensitivityY = 5f;

    [Header("MaxAngle")]
    public float m_maxX = 360.0f;
    public float m_maxY = 50.0f;

    [Header("MinAngle")]
    public float m_minX = -360.0f;
    public float m_minY = 0.0f;


    [HideInInspector]
    private Vector2 m_axis = Vector2.zero;

    bool m_lock = true;//카메라 회전 잠금
    bool m_changeCamera = false;//3인칭 여부(Default 1인칭)

    string InputX = "Mouse X";
    string InputY = "Mouse Y";


    private GameObject m_cameraPosition;//모든 카메라(1,3인칭)의 위치
    private GameObject m_camera1;
    private GameObject m_camera2;

    public void Initialize()//변수 초기화
    {
        foreach (Camera c in Camera.allCameras)
        {
            if (c.tag == "First Person Camera")//1인칭 카메라의 부모 객체를 가져옴
            {
                m_camera1 = c.transform.parent.gameObject;
                m_camera1.transform.rotation = Quaternion.identity;
            }

            if (c.tag == "Third Person Camera")//3인칭 카메라의 부모 객체를 가져옴
            {
                m_camera2 = c.transform.parent.gameObject;
                m_camera2.transform.rotation = Quaternion.identity;
            }
        }

        foreach (Camera c in Camera.allCameras)//해당 씬의 활성화 되어 있는 모든 카메라들
        {
            c.enabled = false;
        }

        m_cameraPosition = m_camera1.transform.parent.gameObject;

        ChangeCamera(true);//시점 초기화(다른 카메라들 Disable) 기본 1인칭

        ToggleCameraLock();//마우스 고정
    }


    public void ChangeCamera(bool f5)//시점 전환 함수
    {
        if (f5)
        {
            m_changeCamera = !m_changeCamera;

            if(m_changeCamera)
            {
                m_camera1.transform.GetChild(0).GetComponent<Camera>().enabled = true;
                m_camera2.transform.GetChild(0).GetComponent<Camera>().enabled = false;
            }
            else
            {
                m_camera2.transform.GetChild(0).GetComponent<Camera>().enabled = true;
                m_camera1.transform.GetChild(0).GetComponent<Camera>().enabled = false;
            }

        }
    }

    public void ToggleCameraLock()//카메라 잠금 온오프
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

    public void UpdateCamera()//카메라 업데이트
    {
        if (!m_lock)
        {
            RotateCamera();
            MoveCamera();
        }
    }

    public Quaternion GetDirection()//카메라 각도 리턴, y값만
    {
        return Quaternion.Euler(0, m_camera1.transform.rotation.eulerAngles.y, 0);//아무 카메라든 상관없음 Y축 rotarion
    }

    private void MoveCamera()//카메라 위치 변경
    {
        if (m_cameraPosition != null)
            m_cameraPosition.transform.position = transform.position;
    }

    private void RotateCamera()//카메라 회전
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
            m_axis.x = Mathf.Clamp(m_axis.x, m_minX, m_maxX);//최대 최소 각도 제한
            m_axis.y = Mathf.Clamp(m_axis.y, m_minY, m_maxY);

            Quaternion rot = Quaternion.Euler(m_axis.y, m_axis.x, 0);
            m_camera1.transform.rotation = Quaternion.Slerp(m_camera1.transform.rotation, rot, 16 * Time.deltaTime);//1인칭 카메라 회전
            m_camera2.transform.rotation = Quaternion.Slerp(m_camera2.transform.rotation, rot, 16 * Time.deltaTime); //3인칭 카메라 회전
        }
    }
}
