using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponControl : MonoBehaviour
{
    private ShootingScripts shootingScripts;

    private void Awake()
    {
        shootingScripts = GetComponentInChildren<ShootingScripts>();
    }

    public void Shooting(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.phase == InputActionPhase.Started)
        {
            shootingScripts = GetComponentInChildren<ShootingScripts>();
            shootingScripts.CheckAuto();
        }
        else if (callbackContext.phase == InputActionPhase.Performed)
        {
            shootingScripts = GetComponentInChildren<ShootingScripts>();
            shootingScripts.CheckAuto();
            shootingScripts.Fire = true;
        }
        else if (callbackContext.phase == InputActionPhase.Canceled)
        {
            shootingScripts.Fire = false;
        }

    }

    public void Reloading(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.phase == InputActionPhase.Started)
        {
            shootingScripts = GetComponentInChildren<ShootingScripts>();
            shootingScripts.Reloading();
        }
    }
    
}
