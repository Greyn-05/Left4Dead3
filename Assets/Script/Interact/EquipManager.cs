using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class EquipManager : MonoBehaviour
{
    public GameObject Pistol;
    public GameObject Rifle;
    public GameObject Shotgun;
    public GameObject Sniper;

    public void ChangePistol (InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.phase == InputActionPhase.Started)
        {
            Pistol.SetActive (true);
            Rifle.SetActive(false); 
            Shotgun.SetActive (false); 
            Sniper.SetActive (false);
        }
    }
    public void ChangeRifle (InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.phase == InputActionPhase.Started)
        {
            Pistol.SetActive(false);
            Rifle.SetActive(true);
            Shotgun.SetActive(false);
            Sniper.SetActive(false);
        }
    }public void ChangeShotgun (InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.phase == InputActionPhase.Started)
        {
            Pistol.SetActive(false);
            Rifle.SetActive(false);
            Shotgun.SetActive(true);
            Sniper.SetActive(false);
        }
    }public void ChangeSniper (InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.phase == InputActionPhase.Started)
        {
            Pistol.SetActive(false);
            Rifle.SetActive(false);
            Shotgun.SetActive(false);
            Sniper.SetActive(true);
        }
    }

}