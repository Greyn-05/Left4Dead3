using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float m_speed = 1.0f;

    public KeyCode m_keyF = KeyCode.F;
    public KeyCode m_keySpace = KeyCode.Space;
    public KeyCode m_keyShift = KeyCode.LeftShift;

    [Header("Gravity")]
    public float m_gravity = -9.8f;


    [HideInInspector]
    private CharacterController m_cc;
    private Vector3 m_movement;
    private Vector3 m_movePosition;

    private bool m_isWalking;
    private bool m_isRunning;

    string InputH = "Horizontal";
    string InputV = "Vertical";


    private void Start()
    {
        m_cc = GetComponent<CharacterController>();
    
    }

    private void Update()
    {
        UpdateInput();
     
    }
    
    public bool IsWalking()
    {
        return m_isWalking;
    }


    public void UpdateInput()
    {
        m_movement.x = Input.GetAxis(InputH) * m_speed;
        m_movement.z = Input.GetAxis(InputV) * m_speed;
        m_movement.y = 0;

        if (m_movement.magnitude > 0)
        {
            m_isWalking = true;
            m_movePosition.Set(m_movement.x, m_gravity, m_movement.z);
            m_cc.Move(transform.rotation * m_movePosition * Time.deltaTime);
        }
        else
        {
            m_isWalking = false;
        }
    }



}
