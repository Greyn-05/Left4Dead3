using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBullet : MonoBehaviour
{    
    public float maxDistance = 1000000; //최대 사거리 나중에 총기별로 변경할 생각 
    public GameObject decalHitWall;
    public float floatInfrontOfWall;
    public GameObject bloodEffect;
    public LayerMask ignoreLayer;
    private Camera _camera;

    void Start()
    {
        _camera = PlayerManager.Instance.m_cameraManager.GetCamera1();
    }

    void Update()
    {
        Ray ray = _camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxDistance, ~ignoreLayer))
        {
            if (decalHitWall)
            {
                if (hit.transform.tag == "LevelPart")
                {
                    Instantiate(decalHitWall, hit.point + hit.normal * floatInfrontOfWall, Quaternion.LookRotation(hit.normal));
                    Destroy(gameObject);
                }

                if (hit.transform.tag == "Enemy")
                {
                    Instantiate(bloodEffect, hit.point, Quaternion.LookRotation(hit.normal));
                    Destroy(gameObject);
                }
            }
            Destroy(gameObject);
        }
        Destroy(gameObject, 0.1f);
    }
}
