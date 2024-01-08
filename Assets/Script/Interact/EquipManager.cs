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

        }
    }

}