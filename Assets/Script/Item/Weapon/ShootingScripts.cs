using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public bool Fire;

    public TMP_Text Bullet;
    
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
        waitTillNextFire = 0;
        bulletsInTheGun = Gun.nowBulletInTheGun;
        Fire = false;
    }

    private void Update()
    {
        if(waitTillNextFire > 0)
        {
            waitTillNextFire -= Time.deltaTime;
        }
        if (Fire)
        {
            if (Gun.gunStyle == GunStyle.automatic)
            {
                AutoShooting();
            }
        }
        Bullet.text = bulletsInTheGun + " / " + Gun.maxBulletInTheGun;

    }
    public void CheckAuto()
    {
        if (Gun.gunStyle == GunStyle.nonautomatic)
        {
            Shooting();
        }
        if (Gun.gunStyle == GunStyle.automatic)
        {
            AutoShooting();
        }
    }

    public void Shooting()
    {
        if(waitTillNextFire <= 0)
        {
            if (bulletsInTheGun != 0)
            {
                ShootMethod();
            }
            else //no more bullet
            {
                reloading = true;
                Reloading();
            }
        }        
    }

    public void AutoShooting()
    {
        if(waitTillNextFire <= 0)
        {
            if (bulletsInTheGun != 0)
            {
                AutoShootMethod();
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
    private void AutoShootMethod()
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
            
            waitTillNextFire = 0.1f;
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
