using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInfo : MonoBehaviour, IOpenDoor
{
    private Animator door_Animator;

    public bool IsOpen { get; set; }

    private void Awake()
    {
        door_Animator = GetComponent<Animator>();
        IsOpen = false;
    }
    public void OpenThisDoor()
    {
        door_Animator.SetTrigger("Open");
        IsOpen = true;
    }
    public void CloseThisDoor()
    {
        door_Animator.SetTrigger("Close");
        IsOpen = false;
    }
}
