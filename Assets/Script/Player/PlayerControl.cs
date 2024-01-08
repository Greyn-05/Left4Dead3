using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    public float m_speed = 1.0f;

    [Header("ChangeCameraKey")]
    public KeyCode m_keyF5 = KeyCode.F5;

    [Header("JumpKey")]
    public KeyCode m_keySpace = KeyCode.Space;

    [HideInInspector]
    private CharacterController m_cc;

    private float m_movementY = 0.0f;//점프구현을 위해 Y방향은 따로 
    private Vector3 m_movement;//캐릭터 Transform 기준 움직이려는 방향
    private Vector3 m_movePosition;//World Transform 기준 움직이게 하려는 방향

    private float m_gravity = -9.8f;

    private void Start()
    {
        m_cc = GetComponent<CharacterController>();
    }

    //캐릭터의 이동거리를 리턴
    public float GetMagnitude()
    {
        return m_movement.magnitude;
    }

    //캐릭터가 이동하고 있는 방향을 리턴
    public float GetDirection()
    {
        return GetMagnitude() > 0 ? Mathf.Atan2(m_movePosition.x, m_movePosition.z) * Mathf.Rad2Deg : 0;
    }

    //점프 체크
    public void IsJumped(InputAction.CallbackContext context)
    {
        m_movementY = m_cc.isGrounded ? 3.0f : m_movementY;//점프키를 눌렀을 때 점프
    }



    //마우스 클릭 체크
    public bool GetMouse0()
    {
        if (Input.GetMouseButton(0))
        {
            return true;
        }
        return false;
    }

    //캐릭터와 땅과의 거리를 리턴
    public float GetGroundDistance()
    {
        RaycastHit hit;
        float dis = 0.0f;
        Ray ray = new Ray(transform.position, Vector3.down);//캐릭터 위치에서 y -1방향으로 레이 생성

        if (Physics.Raycast(ray.origin, ray.direction, out hit, 10))
            dis = ray.origin.y - hit.point.y;

        return dis;
    }

    //캐릭터가 땅에 닿아있는지 확인
    public bool IsGrounded()
    {
        return m_cc.isGrounded;
    }

    //캐릭터를 회전
    public void Rotate(Quaternion angle)
    {
        transform.rotation = angle;
    }

    //캐릭터의 움직임(앞뒤좌우)을 업데이트함
    public void UpdateInput()
    {
        //m_movement.x = Input.GetAxis(InputH) * m_speed;
        //m_movement.z = Input.GetAxis(InputV) * m_speed;

        m_movementY += IsGrounded() ? 0 : (m_gravity * Time.deltaTime);//땅에 안닿았을 경우, 중력 받기

        m_movePosition.Set(m_movement.x, m_movementY, m_movement.z);
        m_cc.Move(transform.rotation * m_movePosition * Time.deltaTime);
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        Vector2 input = Vector2.zero;
        input = context.ReadValue<Vector2>() * m_speed;

        m_movement.x = input.x;
        m_movement.z = input.y;
    }

}
