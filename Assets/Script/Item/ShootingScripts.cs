using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShootingScripts : MonoBehaviour
{
    public GunData GunData;

    private float waitTillNextFire;
    public float roundsPerSecond;

    public GameObject bullet;

    public float bulletsInTheGun = 10;

    public AudioSource shoot_sound_source, reloadSound_source;

    [HideInInspector]
    public bool reloading;

    [Header("탄피")]
    public GameObject bulletSpawnPlace;

    [Header("총구 화염")]
    public GameObject muzzelSpawn;
    private GameObject holdFlash;
    public GameObject[] muzzelFlash;
    
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }


    public void Shooting(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.phase == InputActionPhase.Started /*&& EquipManager.instance.curEquip != null*/)
        {
            if (bulletsInTheGun != 0)
            {
                if (GunData.gunStyle == GunStyle.nonautomatic)
                {
                        ShootMethod();
                }
                if (GunData.gunStyle == GunStyle.automatic)
                {
                        ShootMethod();
                }
            }
            //격발 오류 효과음
        }
        //waitTillNextFire -= roundsPerSecond * Time.deltaTime;
    }
    private void ShootMethod()
    {
        if (waitTillNextFire <= 0 && !reloading)
        {
            int randomNumberForMuzzelFlash = Random.Range(0, 5);
            if (bullet)
                Instantiate(bullet, bulletSpawnPlace.transform.position, bulletSpawnPlace.transform.rotation);
            else
                print("Missing the bullet prefab");
            holdFlash = Instantiate(muzzelFlash[randomNumberForMuzzelFlash], muzzelSpawn.transform.position /*- muzzelPosition*/, muzzelSpawn.transform.rotation * Quaternion.Euler(0, 0, 90)) as GameObject;
            holdFlash.transform.parent = muzzelSpawn.transform;
            if (shoot_sound_source)
                shoot_sound_source.Play();
            animator.SetTrigger("Shot");
            //else
            //    print("Missing 'Shoot Sound Source'.");

            //waitTillNextFire = 1;
            bulletsInTheGun -= 1;
        }

        //else
        //{
        //    //if(!aiming)
        //    StartCoroutine("Reload_Animation");
        //    //if(emptyClip_sound_source)
        //    //	emptyClip_sound_source.Play();
        //}



    }
}
