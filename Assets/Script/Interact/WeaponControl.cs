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
        if (callbackContext.phase == InputActionPhase.Started /*&& EquipManager.instance.curEquip != null*/)
        {
            shootingScripts = GetComponentInChildren<ShootingScripts>();
            shootingScripts.Shooting();
        }
    }

}
