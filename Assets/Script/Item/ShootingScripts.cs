using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShootingScripts : MonoBehaviour
{

    public GunStyles currentStyle; //단발, 연발

    public bool meeleAttack;

    private float waitTillNextFire;
    public float roundsPerSecond;

    public GameObject bullet;

    public AudioSource shoot_sound_source, reloadSound_source;

    [HideInInspector]
    public bool reloading;

    [HideInInspector] public GameObject bulletSpawnPlace;

    public float bulletsInTheGun = 5;

    public GameObject muzzelSpawn;
    private GameObject holdFlash;
    private GameObject holdSmoke;
    public GameObject[] muzzelFlash;

    
    public void Shooting(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.phase == InputActionPhase.Started && EquipManager.instance.curEquip!= null)
        {
            if (!meeleAttack)
            {
                if (currentStyle == GunStyles.nonautomatic)
                {
                        ShootMethod();
                }
                if (currentStyle == GunStyles.automatic)
                {
                        ShootMethod();
                }
            }
        }
        waitTillNextFire -= roundsPerSecond * Time.deltaTime;
    }
    private void ShootMethod()
    {
        if (waitTillNextFire <= 0 && !reloading)
        {

            if (bulletsInTheGun > 0)
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
                else
                    print("Missing 'Shoot Sound Source'.");

                waitTillNextFire = 1;
                bulletsInTheGun -= 1;
            }

            else
            {
                //if(!aiming)
                StartCoroutine("Reload_Animation");
                //if(emptyClip_sound_source)
                //	emptyClip_sound_source.Play();
            }

        }

    }
}
