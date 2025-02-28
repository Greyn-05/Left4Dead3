﻿using UnityEngine;
using UnityEngine.SceneManagement;

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
    public static PlayerManager Instance;
    public HpBar hpBar;

    private PlayerControl m_playerController;
    private WeaponControl m_weaponControl;
    private PlayerAnimationManager m_animationManager;
    private InteractionManager m_interactionManager;

    [HideInInspector]
    public PlayerCameraManager m_cameraManager;

    private float m_healthPoint;
    private float m_maxHp;

    private PlayerState m_state = PlayerState.Idle;

    [HideInInspector]
    public GunData m_mainWeapon = null; //주무기 

    [HideInInspector]
    public GunData m_subWeapon = null; // 보조무기

    private bool m_oneOff = true;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        m_maxHp = 100.0f;
        m_healthPoint = m_maxHp; // 최대 체력으로 시작
        
        m_playerController = GetComponent<PlayerControl>();
        m_cameraManager = GetComponent<PlayerCameraManager>();
        m_animationManager = transform.GetChild(0).GetComponent<PlayerAnimationManager>();
        m_interactionManager = GetComponent<InteractionManager>();
        //m_weaponControl = m_cameraManager.GetWeaponObj().GetComponent<WeaponControl>();

        m_cameraManager.Initialize();
        m_interactionManager.Initialize();
    }

    private void Update()
    {
        hpBar.UpdateHpBar(m_healthPoint);
        if (m_healthPoint > 0)//캐릭터의 체력이 있을 때
        {
            //SelectItem(m_playerController.GetNumKey());
            m_cameraManager.UpdateCamera();
            m_playerController.Rotate(m_cameraManager.GetDirection());
            m_playerController.UpdateInput();
            m_animationManager.UpdateAnimation(m_state, m_playerController.GetDirection(), m_playerController.GetMagnitude(), m_playerController.IsGrounded());//캐릭터의 현재 상태에 따라 애니메이션 전환
        }

        if (m_healthPoint <= 0 && m_oneOff)
        {
            m_cameraManager.ChangeCamera();
            m_cameraManager.ToggleCameraLock();//카메라 회전, 움직임 고정
            m_animationManager.DoDie();

            m_state = PlayerState.Dead;//캐릭터의 상태 = 죽음
            m_oneOff = false;
            Invoke("DieScene", 3f);
        }

    }
    void DieScene()
    {
        SceneManager.LoadScene("Die");
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

    //무기 교체
    public void SwitchWeapon(GunData info)
    {
        if (info.gunType != GunType.Pistol)
        {
            m_mainWeapon = info;//주무기(스나, 샷건 등) 저장
        }
        else
        {
            m_subWeapon = info;//권총만 따로 빼서 저장
        }
    }

    public void ToggleCamera()
    {
        m_cameraManager.ToggleCameraLock();
    }

    //회복킷 추가, 감소
    public void AddAids(int k)
    {
        //m_aidStack += k;
    }

    //수류탄 추가, 감소
    public void AddGrenade(int k)
    {
        //m_grenadeStack += k;
    }
}
