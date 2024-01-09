using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetTheGun : MonoBehaviour
{
    
    public GameObject currentGun; 

    private int currentGunCounter = 0;

    public List<string> gunsIHave = new List<string>();

    public float switchWeaponCooldown;


    //이미 소지하고 있는 무기 선택
    void Create_Weapon()
    {
        /*
		 * Scrolling wheel waepons changing
		 */
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            switchWeaponCooldown = 0;

            currentGunCounter++;
            if (currentGunCounter > gunsIHave.Count - 1)
            {
                currentGunCounter = 0;
            }
            StartCoroutine("Spawn", currentGunCounter);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            switchWeaponCooldown = 0;

            currentGunCounter--;
            if (currentGunCounter < 0)
            {
                currentGunCounter = gunsIHave.Count - 1;
            }
            StartCoroutine("Spawn", currentGunCounter);
        }

        /*
		 * Keypad numbers
		 */
        if (Input.GetKeyDown(KeyCode.Alpha1) && currentGunCounter != 0)
        {
            switchWeaponCooldown = 0;
            currentGunCounter = 0;
            StartCoroutine("Spawn", currentGunCounter);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && currentGunCounter != 1)
        {
            switchWeaponCooldown = 0;
            currentGunCounter = 1;
            StartCoroutine("Spawn", currentGunCounter);
        }

    }
}

