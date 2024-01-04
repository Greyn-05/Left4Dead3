using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShootingScripts : MonoBehaviour
{
    public GunData Gun;

    private float waitTillNextFire; //기본 사격대기시간
    public float roundsPerSecond; //총기별 사격대기 시간 단축

    public GameObject bullet;
    public float bulletsInTheGun;

    private bool reloading;

    [Header("탄피")]
    public GameObject bulletSpawnPlace;

    [Header("총구 화염")]
    public GameObject muzzelSpawn;
    private GameObject holdFlash;
    public GameObject[] muzzelFlash;

    [Header("Sounds")]
    public AudioSource ShotSound, ReloadSound;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        waitTillNextFire = 1;
        bulletsInTheGun = Gun.nowBulletInTheGun;
    }

    private void Update()
    {
        if(waitTillNextFire > 0)
        {
            waitTillNextFire -= roundsPerSecond * Time.deltaTime;
        }
    }

    public void Shooting()
    {
        if(waitTillNextFire <= 0)
        {
            if (bulletsInTheGun != 0)
            {
                if (Gun.gunStyle == GunStyle.nonautomatic)
                {
                    ShootMethod();
                }
                if (Gun.gunStyle == GunStyle.automatic)
                {
                    ShootMethod();
                }
            }
            else //no more bullet
            {
                reloading = true;
                Reloading();
            }
        }        
    }

    private void ShootMethod()
    {
        if (waitTillNextFire <= 0 && !reloading)
        {
            int randomNumberForMuzzelFlash = Random.Range(0, muzzelFlash.Length);
            if (bullet)
                Instantiate(bullet, bulletSpawnPlace.transform.position, bulletSpawnPlace.transform.rotation);
            else
                print("Missing the bullet prefab");
            holdFlash = Instantiate(muzzelFlash[randomNumberForMuzzelFlash], muzzelSpawn.transform.position /*- muzzelPosition*/, muzzelSpawn.transform.rotation * Quaternion.Euler(0, 0, 90)) as GameObject;
            holdFlash.transform.parent = muzzelSpawn.transform;
            if (ShotSound)
                ShotSound.Play();
            animator.SetTrigger("Shot");
            
            waitTillNextFire = 1;
            bulletsInTheGun -= 1;
        }
                

    }

    public void Reloading()
    {
        animator.SetTrigger("Reload");
        if (ReloadSound) ReloadSound.Play();
        else Debug.Log("No ReloadSound");
        StartCoroutine("FillBullet");
    }

    IEnumerator FillBullet()
    {
        yield return new WaitForSeconds(1f);
        bulletsInTheGun = Gun.maxBulletInTheGun;
        reloading = false; 
    }
}
